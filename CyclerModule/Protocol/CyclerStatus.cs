using CyclerModule.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 장비 상태
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CyclerStatus : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public CyclerState State;           // 사이클러 상태 코드
        public EmgState EmgStatus;          // 비상정지 상태
        public int FaultCode;               // 보드 에러 코드
        public CellState CellStatus;         // 셀 코드
        public int TotalStepCount;          // 총 스텝 진행 수
        public int CycleRepeatCount;        // 현재 사이클 설정 반복 횟수
        public int CycleJumpCount;          // 현재 사이클 설정 점프 횟수
        public int CycleNo;                 // 현재 사이클 번호
        public int CycleRepeatNo;           // 현재 사이클 반복 누적 횟수
        public int CycleJumpNo;             // 현재 사이클 점프 누적 횟수
        public int StepNo;                  // 현재 스텝 번호
        public int TotalDays;               // 공정 누적 일                  (day)
        public int TotalTime;               // 공정 누적 시간                (ms)
        public int StepDays;                // 스텝 누적 알                  (day)
        public int StepTime;                // 스텝 누적 시간                (ms)
        public int Volt;                    // 전압                          (0.1 mV)
        public int Curr;                    // 전류                          (0.1 mA)
        public int Power;                   // 전력                          (0.1 mW)
        public int Watthour;                // 전력량                        (0.1 mWh)
        public int Capacity;                // 용량                          (0.1 mAh)
        public int Temperature;             // 온도                          (0.1 ℃)
        public int Resistance;              // 저항                          (uΩ)
        public int ReserveStatus;           // 작업 상태변경 예약 상태
        private int _chamberGroupStatus;    // 챔버 연동 상태                                 [0: 연동 X / 1: 연동]
        private int _chamberStatus;         // 챔버 연결 상태                                 [-1: 무시 / 0: 연결 X / 1: 연결 / 2: 동작 중]
        public int ChamberTemperature;      // 챔버 온도                     (0.1 ℃)
        public int ChamberSettingTemp;      // 챔버 설정 온도                (0.1 ℃)
        private int _inProcessStatus;       // 공정 진행중 상태                               [0: 진행 X / 1: 진행 중]
        public int StepCount;               // 총 스텝 수
        public double RealClock;            // 현재 시간                     (TimeStamp)
        public int FwVersion;               // FW 버전                       (YYYYMMDD)

        int[] spare;                        // 스페어 2개

        public int ChamberGroupStatus
        {
            get { return _chamberGroupStatus; }
            set
            {
                if (0 <= value && value <= 1) _chamberGroupStatus = value;
                //else _chamberGroupStatus = 0;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int ChamberStatus
        {
            get { return _chamberStatus; }
            set
            {
                if (-1 <= value && value <= 1) _chamberStatus = value;
                //else _chamberStatus = 0;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int InProcessStatus
        {
            get { return _inProcessStatus; }
            set
            {
                if (0 <= value && value <= 1) _inProcessStatus = value;
                //else _inProcessStatus = 0;
                else throw new InvalidOperationException("Over Range");
            }
        }

        public CyclerStatus()
        {
            spare = new int[2];
        }

        /// <summary>
        /// 클래스 사이즈 계산
        /// </summary>
        /// 
        /// <returns>
        /// 클래스의 사이즈 값
        /// </returns>
        public int SizeOf()
        {
            BindingFlags AllField = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            int fieldArrCnt = 0;

            // 클래스 내에 있는 모든 필드정보에 대해 반복
            foreach (FieldInfo field in GetType().GetFields(AllField))
            {
                // 클래스 내의 배열 개수 파악
                if (field.FieldType.IsArray)
                    fieldArrCnt++;
            }

            // 사이즈 계산시 배열은 주소값(4byte)으로 계산하므로
            // 클래스에 있는 배열의 수만큼 사이즈에서 차감
            int size = Marshal.SizeOf(this) - (Marshal.SizeOf(typeof(IntPtr)) * fieldArrCnt);

            // 계산된 사이즈에서 배열의 개수만큼 더하기
            size = size + (Marshal.SizeOf(typeof(int)) * spare.Length);

            return size;
        }

        /// <summary>
        /// 클래스의 데이터를 바이트 배열로 변환<br/>
        /// 4-3-2-1 형식으로 변환
        /// </summary>
        /// 
        /// <returns>
        /// 변환된 바이트 배열
        /// </returns>
        public byte[] ToByteArray()
        {
            List<byte> byteStream = new List<byte>();

            foreach (var value in this)
            {
                byte[] tempArr;

                // 필드의 타입에 따른 분류
                switch (value)
                {
                    case CyclerState state:
                        tempArr = BitConverter.GetBytes((int)state);
                        break;
                    case EmgState emg:
                        tempArr = BitConverter.GetBytes((int)emg);
                        break;
                    case CellState cell:
                        tempArr = BitConverter.GetBytes((int)cell);
                        break;
                    case int intVal:
                        tempArr = BitConverter.GetBytes(intVal);
                        break;
                    case double dbVal:
                        tempArr = BitConverter.GetBytes(dbVal);
                        break;
                    default:
                        continue;
                }

                byteStream.AddRange(tempArr);
            }

            return byteStream.ToArray();
        }

        /// <summary>
        /// 바이트 배열을 클래스 데이터로 변환<br/>
        /// 클래스 사이즈와 배열 길이가 일치해야 정상 작동
        /// </summary>
        /// 
        /// <param name="byteArray">변환시킬 바이트 배열</param>
        /// <returns>
        /// 변환 완료 여부
        /// </returns>
        public bool ToClass(byte[] byteArray)
        {
            if (byteArray.Length != SizeOf())
                return false;

            List<byte> tempArray = new List<byte>(byteArray);

            State              = (CyclerState)SliceAndConvert(ref tempArray, typeof(int));
            EmgStatus          = (EmgState)SliceAndConvert(ref tempArray, typeof(int));
            FaultCode          = (int)SliceAndConvert(ref tempArray, typeof(int));
            CellStatus         = (CellState)SliceAndConvert(ref tempArray, typeof(int));
            TotalStepCount     = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleRepeatCount   = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleJumpCount     = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleNo            = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleRepeatNo      = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleJumpNo        = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepNo             = (int)SliceAndConvert(ref tempArray, typeof(int));
            TotalDays          = (int)SliceAndConvert(ref tempArray, typeof(int));
            TotalTime          = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepDays           = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepTime           = (int)SliceAndConvert(ref tempArray, typeof(int));
            Volt               = (int)SliceAndConvert(ref tempArray, typeof(int));
            Curr               = (int)SliceAndConvert(ref tempArray, typeof(int));
            Power              = (int)SliceAndConvert(ref tempArray, typeof(int));
            Watthour           = (int)SliceAndConvert(ref tempArray, typeof(int));
            Capacity           = (int)SliceAndConvert(ref tempArray, typeof(int));
            Temperature        = (int)SliceAndConvert(ref tempArray, typeof(int));
            Resistance         = (int)SliceAndConvert(ref tempArray, typeof(int));
            ReserveStatus      = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChamberGroupStatus = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChamberStatus      = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChamberTemperature = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChamberSettingTemp = (int)SliceAndConvert(ref tempArray, typeof(int));
            InProcessStatus    = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepCount          = (int)SliceAndConvert(ref tempArray, typeof(int));
            RealClock          = (double)SliceAndConvert(ref tempArray, typeof(double));
            FwVersion          = (int)SliceAndConvert(ref tempArray, typeof(int));

            for(int i = 0; i < spare.Length; i++)
                spare[i] = (int)SliceAndConvert(ref tempArray, typeof(int));

            if (tempArray.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 배열을 타입크기만큼 슬라이싱하고 변환하는 함수 
        /// </summary>
        /// 
        /// <param name="byteArray">변환시킬 바이트 리스트의 ref값</param>
        /// <param name="type">변환시킬 자료타입</param>
        /// 
        /// <returns>
        /// 변환된 데이터
        /// </returns>
        object SliceAndConvert(ref List<byte> byteArray, Type type)
        {
            int size = Marshal.SizeOf(type);

            byte[] sliceArray = byteArray.GetRange(0, size).ToArray();
            byteArray.RemoveRange(0, size);

            if (type == typeof(int))
                return BitConverter.ToInt32(sliceArray, 0);
            if (type == typeof(double))
                return BitConverter.ToDouble(sliceArray, 0);
            else
                return null;
        }

        public IEnumerator GetEnumerator()
        {
            yield return State;
            yield return EmgStatus;
            yield return FaultCode;
            yield return CellStatus;
            yield return TotalStepCount;
            yield return CycleRepeatCount;
            yield return CycleJumpCount;
            yield return CycleNo;
            yield return CycleRepeatNo;
            yield return CycleJumpNo;
            yield return StepNo;
            yield return TotalDays;
            yield return TotalTime;
            yield return StepDays;
            yield return StepTime;
            yield return Volt;
            yield return Curr;
            yield return Power;
            yield return Watthour;
            yield return Capacity;
            yield return Temperature;
            yield return Resistance;
            yield return ReserveStatus;
            yield return ChamberGroupStatus;
            yield return ChamberStatus;
            yield return ChamberTemperature;
            yield return ChamberSettingTemp;
            yield return InProcessStatus;
            yield return StepCount;
            yield return RealClock;
            yield return FwVersion;

            foreach (var val in spare)
                yield return val;
        }
    }
}

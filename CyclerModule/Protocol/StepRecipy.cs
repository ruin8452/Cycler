using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 스텝 구조체
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Step
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int CycleNo;                 // 사이클 번호
        public int StepNo;                  // 스텝 번호
        private int _runType;               // 충방전 타입                                    [0: SKIP, 1: 충전, 2: 방전, 3: 휴지, 4: OCV, 5: 패턴, 6: 사용자패턴, 7: Cycle, 8: Loop, 9: End]
        private int _ctrlMode;              // 운전 제어모드                                  [0: 없음, 1: CC, 2: CC/CV, 3: (사용안함), 4: CP, 5: CR]
        public int SetVolt;                 // 설정 전압                     (0.1 mV)
        public int SetCurr;                 // 설정 전류                     (0.1 mV)
        public int SetPower;                // 설정 전력                     (0.1 mW)
        public int SetResistance;           // 설정 저항                     (uΩ)
        public int SetChamberTemp;          // 설정 챔버 온도                (0.1 ℃)
        public int SetChamberHum;           // 설정 챔버 습도                (0.1 %)
        public int SetChamberWaitRate;      // 챔버 온도 설정 대기 시간 배율 (0.1 배)         [0:대기 안함, 기타 : 0.01 배율 값 (예, 1.2)]
        public int SetChamberRange;         // 챔버 온도변화 이상 검출값     (0.1 ℃)          [설정 온도 도달 시 +- 오차 값으로 체크]
        public int SetRecTime;              // 시간 기록 조건                (ms)
        public int SetRecVolt;              // 전압 기록 조건                (0.1 mV)
        public int SetRecCurr;              // 전류 기록 조건                (0.1 mV)
        public int SetRecTemp;              // 온도 기록 조건                (0.1 ℃)
        public int CycleRepeateCount;       // 사이클 반복 횟수
        public int CycleGotoStep;           // 사이클 완료 후 이동 스텝 번호
        public int CycleGotoCount;          // 사이클 완료 후 Goto 횟수
        private int _cycleAccCapaInit;      // 누적용량 초기화                                [0: 누적용량 초기화 안함, 1: 누적용량 초기화]

        public int spare1;                  // 스페어
        public int spare2;                  // 스페어

        public int RunType
        {
            get { return _runType; }
            set
            {
                if (0 <= value && value <= 9) _runType = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int CtrlMode
        {
            get { return _ctrlMode; }
            set
            {
                if (0 <= value && value <= 5) _ctrlMode = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int CycleAccCapaInit
        {
            get { return _cycleAccCapaInit; }
            set
            {
                if (0 <= value && value <= 1) _cycleAccCapaInit = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
    }

    /// <summary>
    /// 스텝 공정 정보
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class StepRecipy : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        private int _stepCount;             // 총 스텝 개수                                   [최대 300]
        public int StartStepNo;             // 시작 스탭번호                                  [0: 처음부터 시작]
        public int StartRepeatCount;        // 시작 반복 횟수
        public Step[] StepInfo;             // 스텝 정보                                      [최대 300개]

        public int StepCount
        {
            get { return _stepCount; }
            set
            {
                if (3 <= value && value <= 300) _stepCount = value;
                else throw new InvalidOperationException("Over Range");
            }
        }

        public StepRecipy()
        {
            StepInfo = new Step[] { };
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
            size = size + (Marshal.SizeOf(typeof(int))) + (Marshal.SizeOf(typeof(Step)) * StepInfo.Length);

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
                    case int intVal:
                        tempArr = BitConverter.GetBytes(intVal);
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
            throw new NotImplementedException();
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
            if (StepInfo == null || StepInfo.Length == 0)
                throw new InvalidOperationException();

            yield return StepCount;
            yield return StartStepNo;
            yield return StartRepeatCount;

            foreach (var val in StepInfo)
            {
                yield return val.CycleNo;
                yield return val.StepNo;
                yield return val.RunType;
                yield return val.CtrlMode;
                yield return val.SetVolt;
                yield return val.SetCurr;
                yield return val.SetPower;
                yield return val.SetResistance;
                yield return val.SetChamberTemp;
                yield return val.SetChamberHum;
                yield return val.SetChamberWaitRate;
                yield return val.SetChamberRange;
                yield return val.SetRecTime;
                yield return val.SetRecVolt;
                yield return val.SetRecCurr;
                yield return val.SetRecTemp;
                yield return val.CycleRepeateCount;
                yield return val.CycleGotoStep;
                yield return val.CycleGotoCount;
                yield return val.CycleAccCapaInit;
                yield return val.spare1;
                yield return val.spare2;
            }
        }
    }
}

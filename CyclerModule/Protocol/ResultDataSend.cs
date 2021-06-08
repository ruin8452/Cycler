using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 결과 데이터 전송
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ResultDataSend : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int TotalStepCount;          // 총 스텝 진행 수
        public int CycleRepeatCount;        // 현재 사이클 총 반복 횟수
        public int CycleJumpCount;          // 현재 사이클 총 점프 횟수
        public int CycleNo;                 // 현재 사이클 번호
        public int CycleRepeatNo;           // 현재 사이클 반복 누적 횟수
        public int CycleJumpNo;             // 현재 사이클 점프 누적 횟수
        public int StepNo;                  // 현재 스텝 번호                                 [1~9999: 정상 스텝결과, 10000+ : EMG결과]
        public double StartDateTime;        // 공정 시작 날짜시간            (TimeStamp)      [공정 시작 시간 Timestamp (GUI Bypass)]
        public int TotakDays;               // 공적 누적 일                  (day)
        public int TotalTime;               // 공적 누적 시간                (ms)
        public int StepDays;                // 스텝 누적 일                  (day)
        public int StepTime;                // 스텝 누적 시간                (ms)
        public int Volt;                    // 전압                          (0.1 mV)
        public int Curr;                    // 전류                          (0.1 mA)
        public int Power;                   // 전력                          (0.1 mW)
        public int Watthour;                // 전략량                        (0.1 mWh)
        public int Capa;                    // 용량                          (0.1 mAh)
        public int Temp;                    // 온도                          (0.1 ℃)
        public int Resistance;              // 저항                          (uΩ)
        public int StatusCode;              // 상태코드
        public int CellCode;                // 셀코드
        public int ChamberTemp;             // 챔버 온도                     (0.1 ℃)
        public int StartVolt;               // 시작 전압                     (0.1 mV)
        public int StepCount;               // 총 스텝 수
        public int ChargeCapa;              // 충전용량                      (0.1 mAh)
        public int DischargeCapa;           // 방전용량                      (0.1 mAh)
        public int CVCapa;                  // CV 용량                       (0.1 mAh)
        public int CVTime;                  // CV 시간                       (ms)
        public int[] DCIRVolt;              // DCIR 계산용 전압              (0.1 mV)         [총 11개 (0초 10초 )]
        public int[] DCIRCurr;              // DCIR 계산용 전류              (0.1 mA)         [총 11개 (0초 10초 )]
        public int AccCapa;                 // 누적 용량                     (0.1 mAh)
        public int AccWatthour;             // 누적 전력량                   (0.1 mWh)
        public int ChargeWatthour;          // 충전 전력량                   (0.1 mWh)
        public int DischargeWatthour;       // 방전 전력량                   (0.1 mWh)
        public int AvgVolt;                 // 평균 전압                     (0.1 mV)
        public int AvgCurr;                 // 평균 전류                     (0.1 mA)
        public int TotalCycleCount;         // 총 사이클 진행 수
        public double EndClock;             // 현재 시간                     (TimeStamp)

        public ResultDataSend()
        {
            DCIRVolt = new int[11];
            DCIRCurr = new int[11];
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
            size += (Marshal.SizeOf(typeof(int)) * DCIRVolt.Length) + (Marshal.SizeOf(typeof(int)) * DCIRCurr.Length);

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

            TotalStepCount   = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleRepeatCount = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleJumpCount   = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleNo          = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleRepeatNo    = (int)SliceAndConvert(ref tempArray, typeof(int));
            CycleJumpNo      = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepNo           = (int)SliceAndConvert(ref tempArray, typeof(int));
            StartDateTime    = (double)SliceAndConvert(ref tempArray, typeof(double));
            TotakDays        = (int)SliceAndConvert(ref tempArray, typeof(int));
            TotalTime        = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepDays         = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepTime         = (int)SliceAndConvert(ref tempArray, typeof(int));
            Volt             = (int)SliceAndConvert(ref tempArray, typeof(int));
            Curr             = (int)SliceAndConvert(ref tempArray, typeof(int));
            Power            = (int)SliceAndConvert(ref tempArray, typeof(int));
            Watthour         = (int)SliceAndConvert(ref tempArray, typeof(int));
            Capa             = (int)SliceAndConvert(ref tempArray, typeof(int));
            Temp             = (int)SliceAndConvert(ref tempArray, typeof(int));
            Resistance       = (int)SliceAndConvert(ref tempArray, typeof(int));
            StatusCode       = (int)SliceAndConvert(ref tempArray, typeof(int));
            CellCode         = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChamberTemp      = (int)SliceAndConvert(ref tempArray, typeof(int));
            StartVolt        = (int)SliceAndConvert(ref tempArray, typeof(int));
            StepCount        = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChargeCapa       = (int)SliceAndConvert(ref tempArray, typeof(int));
            DischargeCapa    = (int)SliceAndConvert(ref tempArray, typeof(int));
            CVCapa           = (int)SliceAndConvert(ref tempArray, typeof(int));
            CVTime           = (int)SliceAndConvert(ref tempArray, typeof(int));
            for(int i = 0; i < DCIRVolt.Length; i++)
                DCIRVolt[i] = (int)SliceAndConvert(ref tempArray, typeof(int));
            for (int i = 0; i < DCIRCurr.Length; i++)
                DCIRCurr[i] = (int)SliceAndConvert(ref tempArray, typeof(int));
            AccCapa           = (int)SliceAndConvert(ref tempArray, typeof(int));
            AccWatthour       = (int)SliceAndConvert(ref tempArray, typeof(int));
            ChargeWatthour    = (int)SliceAndConvert(ref tempArray, typeof(int));
            DischargeWatthour = (int)SliceAndConvert(ref tempArray, typeof(int));
            AvgVolt           = (int)SliceAndConvert(ref tempArray, typeof(int));
            AvgCurr           = (int)SliceAndConvert(ref tempArray, typeof(int));
            TotalCycleCount   = (int)SliceAndConvert(ref tempArray, typeof(int));
            EndClock          = (double)SliceAndConvert(ref tempArray, typeof(double));

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
            yield return TotalStepCount;
            yield return CycleRepeatCount;
            yield return CycleJumpCount;
            yield return CycleNo;
            yield return CycleRepeatNo;
            yield return CycleJumpNo;
            yield return StepNo;
            yield return StartDateTime;
            yield return TotakDays;
            yield return TotalTime;
            yield return StepDays;
            yield return StepTime;
            yield return Volt;
            yield return Curr;
            yield return Power;
            yield return Watthour;
            yield return Capa;
            yield return Temp;
            yield return Resistance;
            yield return StatusCode;
            yield return CellCode;
            yield return ChamberTemp;
            yield return StartVolt;
            yield return StepCount;
            yield return ChargeCapa;
            yield return DischargeCapa;
            yield return CVCapa;
            yield return CVTime;
            foreach (var val in DCIRVolt)
                yield return val;
            foreach (var val in DCIRCurr)
                yield return val;
            yield return AccCapa;
            yield return AccWatthour;
            yield return ChargeWatthour;
            yield return DischargeWatthour;
            yield return AvgVolt;
            yield return AvgCurr;
            yield return TotalCycleCount;
            yield return EndClock;
        }
    }
}

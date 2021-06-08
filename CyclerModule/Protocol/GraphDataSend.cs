using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 그래프 데이터 구조체
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GraphData
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
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
        public int ChamberTemp;             // 챔버 온도                     (0.1 ℃)
        public int ChargeCapa;              // 충전용량                      (0.1 mAh)
        public int DischargeCapa;           // 방전용량                      (0.1 mAh)
        public int CVCapa;                  // CV 용량                       (0.1 mAh)
        public int CVTime;                  // CV 시간                       (ms)
        public int AccCapa;                 // 누적 용량                     (0.1 mAh)
        public int AccWatthour;             // 누적 전력량                   (0.1 mWh)
        public int ChargeWatthour;          // 충전 전력량                   (0.1 mWh)
        public int DischargeWatthour;       // 방전 전력량                   (0.1 mWh)
        public int AvgVolt;                 // 평균 전압                     (0.1 mV)
        public int AvgCurr;                 // 평균 전류                     (0.1 mA)
        public double RealClock;            // 현재 시간                     (TimeStamp)
    }

    /// <summary>
    /// 그래프 데이터 전송
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GraphDataSend : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int TotalStepCount;          // 총 스텝 진행 수                            
        public int CycleRepeatCount;        // 현재 사이클 총 반복 횟수                   
        public int CycleJumpCount;          // 현재 사이클 총 점프 횟수                   
        public int CycleNo;                 // 현재 사이클 번호                           
        public int CycleRepeatNo;           // 현재 사이클 반복 누적 횟수                 
        public int CycleJumpNo;             // 현재 사이클 점프 누적 횟수
        public int StepNo;                  // 현재 스텝 번호
        public double StartDateTime;        // 공정 시작 날짜시간            (TimeStamp)      [공정 시작 시간 TimeStamp]
        public int TotalCount;              // 총 데이터 개수                                 [최대 50개(100ms 5초)]
        public int TotalCycleCount;         // 총 사이클 진행 수
        public GraphData[] GraphDatas;      // 그래프 데이터

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
            size += (Marshal.SizeOf(typeof(GraphData)) * GraphDatas.Length);

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
            TotalCount       = (int)SliceAndConvert(ref tempArray, typeof(int));
            TotalCycleCount  = (int)SliceAndConvert(ref tempArray, typeof(int));

            for(int i = 0; i < GraphDatas.Length; i++)
            {
                GraphDatas[i].TotakDays         = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].TotalTime         = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].StepDays          = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].StepTime          = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Volt              = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Curr              = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Power             = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Watthour          = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Capa              = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Temp              = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].Resistance        = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].ChamberTemp       = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].ChargeCapa        = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].DischargeCapa     = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].CVCapa            = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].CVTime            = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].AccCapa           = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].AccWatthour       = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].ChargeWatthour    = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].DischargeWatthour = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].AvgVolt           = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].AvgCurr           = (int)SliceAndConvert(ref tempArray, typeof(int));
                GraphDatas[i].RealClock         = (double)SliceAndConvert(ref tempArray, typeof(double));
            }

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
            if (GraphDatas == null || GraphDatas.Length == 0)
                throw new InvalidOperationException();

            yield return TotalStepCount;
            yield return CycleRepeatCount;
            yield return CycleJumpCount;
            yield return CycleNo;
            yield return CycleRepeatNo;
            yield return CycleJumpNo;
            yield return StepNo;
            yield return StartDateTime;
            yield return TotalCount;
            yield return TotalCycleCount;

            foreach (var val in GraphDatas)
            {
                yield return val.TotakDays;
                yield return val.TotalTime;
                yield return val.StepDays;
                yield return val.StepTime;
                yield return val.Volt;
                yield return val.Curr;
                yield return val.Power;
                yield return val.Watthour;
                yield return val.Capa;
                yield return val.Temp;
                yield return val.Resistance;
                yield return val.ChamberTemp;
                yield return val.ChargeCapa;
                yield return val.DischargeCapa;
                yield return val.CVCapa;
                yield return val.CVTime;
                yield return val.AccCapa;
                yield return val.AccWatthour;
                yield return val.ChargeWatthour;
                yield return val.DischargeWatthour;
                yield return val.AvgVolt;
                yield return val.AvgCurr;
                yield return val.RealClock;
            }
        }
    }
}

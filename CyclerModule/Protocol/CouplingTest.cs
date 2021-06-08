using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 체결 테스트
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CouplingTest : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        private int _setMode;                 // 충방전 모드                                    [1: 충전, 2: 방전, 3: 충/방전]
        public int SetChargeVolt;           // 설정 충전 전압                (0.1 mV)
        public int SetDishargeVolt;         // 설정 방전 전압                (0.1 mV)
        public int SetCurr;                 // 설정 전류                     (0.1 mA)
        public int SetEndTIme;              // 종료 시간                     (ms)
        public int JudgeChargeIRMax;        // 판단조건 - 충전 상한 저항     (uΩ)
        public int JudgeChargeIRMix;        // 판단조건 - 충전 하한 저항     (uΩ)
        public int JudgeDischargeIRMax;     // 판단조건 - 방전 상한 저항     (uΩ)
        public int JudgeDischargeIRMix;     // 판단조건 - 방전 하한 저항     (uΩ)

        public int SetMode
        {
            get { return _setMode; }
            set
            {
                if (1 <= value && value <= 3) _setMode = value;
                else throw new InvalidOperationException("Over Range");
            }
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
            yield return SetMode;
            yield return SetChargeVolt;
            yield return SetDishargeVolt;
            yield return SetCurr;
            yield return SetEndTIme;
            yield return JudgeChargeIRMax;
            yield return JudgeChargeIRMix;
            yield return JudgeDischargeIRMax;
            yield return JudgeDischargeIRMix;
        }
    }
}

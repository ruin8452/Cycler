using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 패턴 정보 구조체
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Pattern
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int PatternTime;             // 패턴 시간                     (ms)
        public int PatternValue;            // 패턴 설정값                   (0.1 mA or mW)         
    }

    /// <summary>
    /// 패턴파일 정보 전송
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PatternFileInfo : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int StepNo;                  // 패턴 스텝 번호
        private int _patternStartIndex;     // 패턴 시작 인덱스                               [1 ~ 50000 (최대 50000개 패턴 데이터 전송)]
        private int _patternCount;          // 패턴 전송 개수                                 [0: 전송완료, 1 ~ 5000 (한 패킷으로 전송 가능한 최대 개수)]
        private int _patternMode;           // 패턴 모드                                      [1: 전류, 2: 전력]
        private int _patternEnd;            // 패턴 데이터 전송 완료                          [0: 데이터 전송 중, 1: 데이터 전송 완료]
        public Pattern[] PatternsInfo;      // 패턴 정보

        public int PatternStartIndex
        {
            get { return _patternStartIndex; }
            set
            {
                if (1 <= value && value <= 50000) _patternStartIndex = value;
                else throw new InvalidOperationException("Over Range");
            }
        }

        public int PatternCount
        {
            get { return _patternCount; }
            set
            {
                if (0 <= value && value <= 50000) _patternCount = value;
                else throw new InvalidOperationException("Over Range");
            }
        }

        public int PatternMode
        {
            get { return _patternMode; }
            set
            {
                if (1 <= value && value <= 2) _patternMode = value;
                else throw new InvalidOperationException("Over Range");
            }
        }

        public int PatternEnd
        {
            get { return _patternEnd; }
            set
            {
                if (0 <= value && value <= 1) _patternEnd = value;
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

            // 계산된 사이즈에서 배열의 개수만큼 더하기
            size = size + (Marshal.SizeOf(typeof(Pattern)) * PatternsInfo.Length);

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
            // 종료 & 보호조건 데이터가 없다면 예외처리
            if (PatternsInfo == null || PatternsInfo.Length == 0)
                throw new InvalidOperationException();

            yield return StepNo;
            yield return PatternStartIndex;
            yield return PatternCount;
            yield return PatternMode;
            yield return PatternEnd;

            foreach (var val in PatternsInfo)
            {
                yield return val.PatternTime;
                yield return val.PatternValue;
            }
        }
    }
}

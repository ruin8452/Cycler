using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /// <summary>
    /// 챔버 그룹정보 전송
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ChamberGroupInfo : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int ChamberGroup;            // 챔버 연동 그룹 정보                            [* 채널보드 ID 1~32까지 bit 값으로 정보 전송]
        public int ChamberGroupUse;         // 챔버 연동 사용 채널정보                        [-1: 연동정보 업데이트 안함, 0~: bit값으로 업데이트]
        private int _chamberGroupID;        // 챔버 그룹 ID                                   [0: 챔버없음, 1~: 챔버 ID]
        private int _chamberMode;           // 챔버 운영 모드                                 [0: 미사용, 1: oven 연동, 2: Step 동기화]

        public int ChamberGroupID
        {
            get { return _chamberGroupID; }
            set
            {
                if (0 <= value) _chamberGroupID = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int ChamberMode
        {
            get { return _chamberMode; }
            set
            {
                if (0 <= value && value <= 2) _chamberMode = value;
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

                //Array.Reverse(tempArr, 0, tempArr.Length);
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
            yield return ChamberGroup;
            yield return ChamberGroupUse;
            yield return ChamberGroupID;
            yield return ChamberMode;
        }
    }
}

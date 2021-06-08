using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 스텝 종료 및 보호조건
    /// <summary>
    /// 스텝 종료 및 보호조건 구조체
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct End_Protect
    {
        // [[[ 종료조건 ]]]                 [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int EndVolt;                 // 전압 종료                     (0.1 mV)         [REST, PATTERN ->종료전압H]
        public int EndVoltMoveStep;         // 전압 종료 시 이동 스텝
        public int EndCurr;                 // 전류 종료                     (0.1 mA)         [REST, PATTERN ->종료전압L]
        public int EndCurrMoveStep;         // 전류 종료 시 이동 스텝
        public int EndTimeDay;              // 시간 종료 - 일                (day)
        public int EndTimeTime;             // 시간 종료 - 시간              (ms)
        public int EndTimeMoveStep;         // 시간 종료 시 이동 스텝
        public int EndCVTimeDay;            // CV시간 종료 - 일              (day)
        public int EndCVTimeTime;           // CV시간 종료 - 시간            (ms)
        public int EndCVTImeMoveStep;       // CV시간 종료 시 이동 스텝
        public int EndCapa;                 // 용량 종료                     (0.1 mAh)
        public int EndCapaMoveStep;         // 용량 종료 시 이동 스텝
        public int EndPower;                // 전력 종료                     (0.1 mW)
        public int EndWatthour;             // 전력량 종료                   (0.1 mWh)
        public int EndDeltaVp;              // Delta VP 종료                 (0.1 mV)
        public int EndTemp;                 // 온도 종료                     (0.1 ℃)
        private int _endTempType;           // 온도 종료 모드                                 [1: 셀 온도, 2: 챔버온도]
        public int EndTempMoveStep;         // 온도 종료 시 이동 스텝
        public int EndSocEiffc;             // SOC 종료 효율                 (0.1 %)
        public int EndSoc;                  // SOC값 적용 스텝                                [0: 미사용, 나머지: 해당 스텝 번호]
        public int EndSocMoveStep;          // SOC 종료 시 이동 스텝
        private int _endSocMode;            // SOC 모드                                       [1: SoC, 2: DoD]
        public int EndStepSave;             // 종료스텝 저장
        private int _endCapaEiffc;          // 용량 효율 모드                                 [0: 미사용, 1: Cycle, 2~: Step 번호]
        public int EndCapaRepeat;           // 용량 효율 반복 횟수
        public int EndCapaStep;             // 용량 효율 적용 스텝
        public int EndCapaValue;            // 용량 효율 값                  (0.1 %)
        public int EndCapaFinish;           // 용량 효율 진행선택
        private int _endEnergyEiffc;        // 에너지 효율 모드                               [0: 미사용, 1: Cycle, 2~: Step 번호]
        public int EndEnergyRepeat;         // 에너지 효율 반복 횟수
        public int EndEnergyStep;           // 에너지 효율 적용 스텝
        public int EndEnergyValue;          // 에너지 효율 값                (0.1 %)
        private int _endEnergyFinish;       // 에너지 효율 진행선택                           [0: 종료, 1: 일시정지, 2: 다음 Step]
        private int _endResistanceEiffc;    // 저항 효율 모드                                 [0: 미사용, 1: Cycle, 2~: Step 번호]
        public int EndResistanceRepeat;     // 저항 효율 반복 횟수
        public int EndResistanceStep;       // 저항 효율 적용 스텝
        public int EndResistanceValue;      // 저항 효율 값                  (0.1 %)
        private int _endResistanceFinish;   // 저항 효율 진행선택                             [0: 종료, 1: 일시정지, 2: 다음 Step]
        public int EndPauseRepeat;          // 사이클 반복 후 일시정지
        public int EndPauseStep;            // 사이클 반복 후 일시정지 스텝
        private int _endSumCapaKind;          // 사이클 누적 용량 항목                          [0: 미사용, 1: 충전, 2: 방전, 3: 충방전]
        public int EndSumCapaValue;         // 사이클 누적 용량 종료값       (0.1 mAh)
        private int _endSumWhKind;            // 사이클 누적 전력량 항목                        [0: 미사용, 1: 충전, 2: 방전, 3: 충방전]
        public int EndSumWhValue;           // 사이클 누적 전력량 종료값     (0.1 mWh)

        // [[[ 보호조건 ]]]                 [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        public int ProtectVoltUpper;        // 전압 상한                     (0.1 mV)
        public int ProtectVoltLower;        // 전압 하한                     (0.1 mV)
        public int ProtectCurrUpper;        // 전류 상한                     (0.1 mA)
        public int ProtectCurrLower;        // 전류 하한                     (0.1 mA)
        public int ProtectCapaUpper;        // 용량 상한                     (0.1 mAh)
        public int ProtectCapaLower;        // 용량 하한                     (0.1 mAh)
        public int ProtectResistanceUpper;  // 임피던스 상한                 (uΩ)
        public int ProtectResistanceLower;  // 임피던스 하한                 (uΩ)
        public int ProtectTempUpper;        // 온도 상한                     (0.1 ℃)
        public int ProtectTempLower;        // 온도 하한                     (0.1 ℃)
        public int ProtectRestartTempUpper; // 재시작 온도 상한              (0.1 ℃)
        public int ProtectRestartTempLower; // 재시장 온도 하한              (0.1 ℃)

        public int EndTempType
        {
            get { return _endTempType; }
            set
            {
                if (1 <= value && value <= 2) _endTempType = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndSocMode
        {
            get { return _endSocMode; }
            set
            {
                if (1 <= value && value <= 2) _endSocMode = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndCapaEiffc
        {
            get { return _endCapaEiffc; }
            set
            {
                if (0 <= value) _endCapaEiffc = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndEnergyEiffc
        {
            get { return _endEnergyEiffc; }
            set
            {
                if (0 <= value) _endEnergyEiffc = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndEnergyFinish
        {
            get { return _endEnergyFinish; }
            set
            {
                if (0 <= value && value <= 2) _endEnergyFinish = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndResistanceEiffc
        {
            get { return _endResistanceEiffc; }
            set
            {
                if (0 <= value) _endResistanceEiffc = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndResistanceFinish
        {
            get { return _endResistanceFinish; }
            set
            {
                if (0 <= value && value <= 2) _endResistanceFinish = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndSumCapaKind
        {
            get { return _endSumCapaKind; }
            set
            {
                if (0 <= value && value <= 3) _endSumCapaKind = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
        public int EndSumWhKind
        {
            get { return _endSumWhKind; }
            set
            {
                if (0 <= value && value <= 3) _endSumWhKind = value;
                else throw new InvalidOperationException("Over Range");
            }
        }
    }

    /// <summary>
    /// 스텝 종료 및 보호조건
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StepEnd_Protect : ICyclerProtocol, IEnumerable
    {
        //                                  [[[ 설명 ]]]                     [[[ 단위 ]]]     [[[ 비고 ]]]
        private int _stepCount;             // 총 스텝 개수                                   [최대 300개]
        public End_Protect[] End_Protects;  // 종료 & 보호조건                                [최대 300개]

        public int StepCount
        {
            get { return _stepCount; }
            set
            {
                if (3 <= value && value <= 300) _stepCount = value;
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
            size = size + (Marshal.SizeOf(typeof(End_Protect)) * End_Protects.Length);

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

            Array.Reverse(sliceArray);

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
            if (End_Protects == null || End_Protects.Length == 0)
                throw new InvalidOperationException();

            yield return StepCount;

            foreach (var val in End_Protects)
            {
                yield return val.EndVolt;
                yield return val.EndVoltMoveStep;
                yield return val.EndCurr;
                yield return val.EndCurrMoveStep;
                yield return val.EndTimeDay;
                yield return val.EndTimeTime;
                yield return val.EndTimeMoveStep;
                yield return val.EndCVTimeDay;
                yield return val.EndCVTimeTime;
                yield return val.EndCVTImeMoveStep;
                yield return val.EndCapa;
                yield return val.EndCapaMoveStep;
                yield return val.EndPower;
                yield return val.EndWatthour;
                yield return val.EndDeltaVp;
                yield return val.EndTemp;
                yield return val.EndTempType;
                yield return val.EndTempMoveStep;
                yield return val.EndSocEiffc;
                yield return val.EndSoc;
                yield return val.EndSocMoveStep;
                yield return val.EndSocMode;
                yield return val.EndStepSave;
                yield return val.EndCapaEiffc;
                yield return val.EndCapaRepeat;
                yield return val.EndCapaStep;
                yield return val.EndCapaValue;
                yield return val.EndCapaFinish;
                yield return val.EndEnergyEiffc;
                yield return val.EndEnergyRepeat;
                yield return val.EndEnergyStep;
                yield return val.EndEnergyValue;
                yield return val.EndEnergyFinish;
                yield return val.EndResistanceEiffc;
                yield return val.EndResistanceRepeat;
                yield return val.EndResistanceStep;
                yield return val.EndResistanceValue;
                yield return val.EndResistanceFinish;
                yield return val.EndPauseRepeat;
                yield return val.EndPauseStep;
                yield return val.EndSumCapaKind;
                yield return val.EndSumCapaValue;
                yield return val.EndSumWhKind;
                yield return val.EndSumWhValue;

                yield return val.ProtectVoltUpper;
                yield return val.ProtectVoltLower;
                yield return val.ProtectCurrUpper;
                yield return val.ProtectCurrLower;
                yield return val.ProtectCapaUpper;
                yield return val.ProtectCapaLower;
                yield return val.ProtectResistanceUpper;
                yield return val.ProtectResistanceLower;
                yield return val.ProtectTempUpper;
                yield return val.ProtectTempLower;
                yield return val.ProtectRestartTempUpper;
                yield return val.ProtectRestartTempLower;
            }
        }
    }
}

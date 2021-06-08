namespace CyclerModule.Code
{
    public enum CyclerState
    {
        DISCONNECT = 0x0000,    // 연결 끊김
        WAIT,                   // 대기
        DATA_CHECK,             // 손실 데이터 체크
        STOP = 0x1001,          // 정지(대기)
        EMG,                    // 비상정지
        INIT,                   // 초기화 중
        PAUSE,                  // 공정 일시 정지
        CHAMBER_TEMP_WAIT,      // 챔버 온도 대기
        FINISH,                 // 공정종료
        RECIPY_SEND,            // 공정정보 전송 중

        // 충전
        CC_CHARGE,              // CC 충전
        CC_CV_CHARGE,           // CC/CV 충전
        CP_CHARGE,              // CP 충전
        CR_CHARGE,              // CR 충전

        // 방전
        CC_DISCHARGE,           // CC 방전
        CC_CV_DISCHARGE,        // CC/CV 방전
        CP_DISCHARGE,           // CP 방전
        CR_DISCHARGE,           // CR 방전

        REST,                   // 휴지
        OCV,                    // OCV
        PRECHARGE,              // 간이충방전
        BALANCING,              // 밸런싱
        CONNET_TEST,            // 체결 테스트
        CALIBRATION,            // 캘
        PATTERN,                // 패턴 공정
        STEP_SYNC_WAIT,         // 스텝 연동 대기(챔버 스텝 동기화)
        DATA_RESTORE_WAIT       // 데이터 복구 대기
    }

    public enum EmgState
    {
        NORMAL,                                 // 정상
        EMG_SWITCH_PUSH,                        // EMG 스위치 눌림
        EMG_BACK_UP_PC,                         // 백업 PC EMG
        EMG_INVERTER,                           // 인버터 상태 이상
        EMG_AC_INPUT,                           // AC 입력전원 이상
        EMG_TEMP_OVER_LIMIT,                    // 온도센서 상한 초과
        EMG_TEMP_UNDER_LIMIT,                   // 온도센서 하한 미달
        EMG_CHAMBER_TEMP,                       // 챔버 온도 이상
        EMG_CHAMBER_SET_TIME_OVER,              // 챔버 온도 설정 시간 초과
        EMG_CELL,                               // 셀 에러
        EMG_PWM_TRIP_ZONE = 200,                // PWM 트립존 에러
        EMG_DCDC_OUT_CURR_OVER_LIMIT,           // DCDC 1차 출력 전류 상한
        EMG_DCDC_OUT_CURR_UNDER_LIMIT,          // DCDC 1차 출력 전류 하한
        EMG_DCDC_OUT_VOLT_OVER_LIMIT,           // DCDC 1차 출력 전압 상한
        EMG_DCDC_OUT_VOLT_UNDER_LIMIT,          // DCDC 1차 출력 전압 하한
        EMG_INV_IN_VOLT_OVER_LIMIT = 301,       // 인버터 입력전압 상한
        EMG_INV_IN_VOLT_UNDER_LIMIT,            // 인버터 입력전압 하한
        EMG_BB_IN_VOLT_OVER_LIMIT = 401,        // 벅부스트 입력전압 상한
        EMG_BB_IN_VOLT_UNDER_LIMIT,             // 벅부스트 입력전압 하한
        EMG_BB_UP_OUT_VOLT_OVER_LIMIT = 501,    // 벅부스트 상부 출력전압 상한
        EMG_BB_UP_OUT_VOLT_UNDER_LIMIT,         // 벅부스트 상부 출력전압 하한
        EMG_BB_DOWN_OUT_VOLT_OVER_LIMIT = 601,  // 벅부스트 하부 출력전압 상한
        EMG_BB_DOWN_OUT_VOLT_UNDER_LIMIT,       // 벅부스트 하부 출력전압 하한
        EMG_BAT_VOLT_OVER_LIMIT = 701,          // 배터리 전압 상한
        EMG_BAT_VOLT_UNDER_LIMIT,               // 배터리 전압 하한
        EMG_OUT_CURR_C_OVER_LIMIT = 801,        // 출력전류 충전 상한
        EMG_OUT_CURR_D_OVER_LIMIT,              // 출력전류 방전 상한
    }

    public enum CellState
    {
        NORMAL = 0x30303030,              // 정상
        CV_TIME_END,                      // CV시간 종료
        VOLT_END,                         // 전압 종료
        CURR_END,                         // 전류 종료
        dV_END,                           // dV 종료
        CAPA_END,                         // 용량 종료
        RESERVE,                          // 미사용
        POWER_END,                        // 전력 종료
        WATTHOUR_END,                     // 전력량 종료
        TEMP_END,                         // 온도 종료
        SOC_DOD_END,                      // SOC, DOD 종료
        TIME_END,                         // 시간 종료

        OVER_CHARGE_PAUSE = 0x50303030,   // CELL 과충전 방지 일시 정지
        OVER_DISCHARGE_PAUSE,             // CELL 과광전 방지 일시 정지
        OVER_TEMP_PAUSE,                  // CELL 과열 방지 일시 정지
        CYCLE_PAUSE,                      // 싸이클 일시 정지(LOOP 조건)
        USER_PAUSE,                       // 사용자 일시 정지(작업 멈춤 기능)

        VOLT_OVER_LIMIT = 0x41533030,     // 전압 상한 초과
        VOLT_UNDER_LIMIT,                 // 전압 하한 미달
        CAPA_OVER_LIMIT,                  // 용량 상한 초과
        CURR_OVER_LIMIT,                  // 전류 상한 초과
        CURR_UNDER_LIMIT,                 // 전류 하한 미달
        TEMP_OVER_LIMIT,                  // 온도 상한 초과

        OCV_VOLT_OVER_LIMIT = 0x43543030, // OCV 전압 상한 초과
        OCV_VOLT_UNDER_LIMIT,             // OCV 전압 하한 미달
        IN_CURR_VOLT_OVER_LIMIT,          // 전류 인가 중 전압 상한 초과
        IN_CURR_VOLT_UNDER_LIMIT,         // 전류 인가 중 전압 하한 미달
        IN_CURR_CURR_OVER_LIMIT,          // 전류 인가 중 전류 상한 초과
        IN_CURR_CURR_UNDER_LIMIT,         // 전류 인가 중 전류 하한 미달
        AFTER_CURR_RES_OVER_LIMIT,        // 전류 인가 후 저항값 상한 초과
        AFTER_CURR_RES_UNDER_LIMIT,       // 전류 인가 후 저항값 하한 미달

        C_VOLT_OVER_LIMIT = 0x43433030,   // 충전 전압 상한 초과
        C_VOLT_UNDER_LIMIT,               // 충전 전압 하한 미달
        C_CURR_OVER_LIMIT,                // 충전 전류 상한 초과
        C_CURR_UNDER_LIMIT,               // 충전 전류 하한 미달(CV 구간)
        C_CAPA_OVER_LIMIT,                // 충전 용량 상한 초과
        C_CAPA_UNDER_LIMIT,               // 충전 용량 하한 초과
        C_RESISTANCE_OVER_LIMIT,          // 충전 중 저항 상한 초과
        C_RESISTANCE_UNDER_LIMIT,         // 충전 중 저항 하한 초과
        C_TEMP_OVER_LIMIT,                // 충전 온도 상한 초과
        C_TEMP_UNDER_LIMIT,               // 충전 온도 하한 초과

        D_VOLT_OVER_LIMIT = 0x43443030,   // 방전 전압 상한 초과
        D_VOLT_UNDER_LIMIT,               // 방전 전압 하한 미달
        D_CURR_OVER_LIMIT,                // 방전 전류 상한 초과
        D_CURR_UNDER_LIMIT,               // 방전 전류 하한 미달(CV 구간)
        D_CAPA_OVER_LIMIT,                // 방전 용량 상한 초과
        D_CAPA_UNDER_LIMIT,               // 방전 용량 하한 초과
        D_RESISTANCE_OVER_LIMIT,          // 방전 중 저항 상한 초과
        D_RESISTANCE_UNDER_LIMIT,         // 방전 중 저항 하한 초과
        D_TEMP_OVER_LIMIT,                // 방전 온도 상한 초과
        D_TEMP_UNDER_LIMIT,               // 방전 온도 하한 초과

        R_VOLT_OVER_LIMIT = 0x43523030,   // 휴지 전압 상한 초과
        R_VOLT_UNDER_LIMIT,               // 휴지 전압 하한 미달
    }
}

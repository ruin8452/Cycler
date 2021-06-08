namespace CyclerModule.Code
{
    public enum ProtocolCommand
    {   //                                  [[[ 설명 ]]]                      [[[ 통신 방향 ]]]   [[[ 프로토콜 존재 여부]]]
        NONE_COMMAND          = 0x0000,
        CYCLER_STATUS         = 0x0001,     // 장비 상태 요청                 PC -> MCU           O
        RECIPY_PROTECT        = 0x0003,     // 보호조건 전송                  PC -> MCU           O
        STEP_RECIPY           = 0x0005,     // 스텝 공정정보 전송             PC -> MCU           O
        STEP_END_PROTECT,                   // 스텝 종료 및 보호조건 전송     PC -> MCU           O
        CONTROL,                            // 제어 명령                      PC -> MCU           O
        SET_CHAMBER,                        // 챔버 설정                      PC -> MCU           O
        PRE_CHARGE_DISCHARGE,               // 간이 충방전                    PC -> MCU           O
        COUPLING_TEST         = 0x0010,     // 체결 테스트                    PC -> MCU           O
        PATTERN_FILE_INFO     = 0x0012,     // 패턴파일 정보 전송             PC -> MCU           O
        CHAMBER_GROUP_INFO,                 // 챔버 그룹정보 전송             PC -> MCU           O
        CAL_POINT_CHECK,                    // 캘 포인트 확인                 PC -> MCU           O
        CAL_POINT_DELETE,                   // 캘 포인트 삭제                 PC -> MCU           O
        CAL_OUTPUT_CHANGE,                  // 캘 출력값 변경                 PC -> MCU           O
        CAL_DMM,                            // 캘 DMM값 적용                  PC -> MCU           O
        CAL_OUTPUT_STOP,                    // 캘 출력 정지                   PC -> MCU           X
        SAFETY_CONDITION,                   // 안전조건 설정                  PC -> MCU           O
        GRAPH_DATA_SEND       = 0x0102,     // 그래프 데이터 전송             MCU -> PC           O
        RESULT_DATA_SEND,                   // 결과 데이터 전송               MCU -> PC           O
        STATUS_ANSWER,                      // 상태 응답                      MCU -> PC           O
        COUPLING_TEST_RESULT,               // 체결 테스트 결과 전송          MCU -> PC           O
        CHAMBER_COMM_LOG,                   // 챔버 통신 로그 전송            MCU -> PC           O

        CAL_MAIN_RELAY        = 0x0201,     // 캘 메인MCU 릴레이 변경         PC -> CAL MCU       O
        CAL_MAIN_DMM                        // 캘 메인MCU DMM요청             PC -> CAL MCU       X
    }

    public enum CtrlCommand
    {
        EMG_STOP = 0x0001,          // 비상정지
        RUN_STOP,                   // 운전정지
        RUN_START,                  // 운전시작
        PAUSE,                      // 일시정지
        RUN_CONTINUE,               // 공정 이어진행
        INIT,                       // 초기화
        PAUSE_RUN_STEP,             // 현재 스텝 운전 후 일시정지
        PAUSE_RUN_CYCLE,            // 현재 사이클 운전 후 일시정지
        PAUSE_RESERVATION_CANCLE,   // 일시정지 예약 취소
        CHAMBER_SYNC_CANCLE,        // 챔버 연동 취소
        CHAMBER_RUN,                // 챔버 운영
        CHAMBER_RUN_STOP,           // 챔버 운영 정지
        NEXT_STEP_RUN,              // 다음 스텝 진행
        STOP_RESERVATION_CANCLE,    // 정지 예약 취소
        STOP_RUN_STEP,              // 현재 스탭 운전 후 정지
        STOP_RUN_CYCLE,             // 현재 사이클 운전 후 정지
        TIME_SYNC,                  // 시간 동기화
        PAUSE_ETC,                  // ETC 일시정지
        BLACKOUT_PAUSE_RELEASE      // 장비정전 일시정지 해제
    }

    public enum StatusCommand
    {
        FINISH = 0x0001,            // 공정 완료
        PRECHARGE_FINISH,           // 간이 충방전 완료
        COUPLING_TEST_FINISH        // 체결 테스트 완료
    }
}

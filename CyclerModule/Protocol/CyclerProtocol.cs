using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CyclerModule.Protocol
{
    // 사이클러 프로토콜 패킷
    //                      HEAD                          DATA           TAIL
    // ┌────────────┬──────────┬────────┬────────┬───────────┬──────┬────────────┐
    // │     STX      Protocal   Return    Data  │  Function   Data │     ETX    │
    // │              Version     Code    Length │    Code          │            │
    // ├────────────┼──────────┼────────┼────────┼───────────┼──────┼────────────┤
    // │    INT32       INT32     INT32   INT32  │   INT32     가변 │    INT32   │
    // ├────────────┼──────────┼────────┼────────┼───────────┼──────┼────────────┤
    // │ 0x7FFE7FFE      1                DATA부 │                  │ 0x7FFE7FFE │
    // │    고정                           길이  │                  │    고정    │
    // └────────────┴──────────┴────────┴────────┴───────────┴──────┴────────────┘

    public interface ICyclerProtocol
    {
        int SizeOf();
        byte[] ToByteArray();
        bool ToClass(byte[] byteArray);
    }
}

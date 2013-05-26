using System;
using System.Collections.Generic;
using System.Text;

namespace Extend
{
    public class Demo : Wlniao.WeChat.ActionBase
    {
        public string Test()
        {
            return "你发送的内容：\n" + MsgText + "\n要查询的内容：\n" + MsgArgs;
        }
    }
}

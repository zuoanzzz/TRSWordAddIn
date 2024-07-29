using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils
{
    public class Constant
    {
        public const string LEXICAL_TYPE = "字词";
        public const string SYNTACTIC_TYPE = "语法";
        public const string SEMANTIC_TYPE = "语义";
        public const string SENSITIVE_TYPE = "敏感";
        public const string OMMONSENSE_TYPE = "字词";

        public static string[] ENGINE_PROVIDER_BASE = { "i3pps", "col", "rule" };

        public const string ENGINE_PROVIDER_T1 = "[\"i3pps\"]";

        public const string ENGINE_PROVIDER_T2 = "[ \"col\", \"rule\"]";

        public const string ENGINE_PROVIDER_CS = "[\"i3pps\", \"col\", \"rule\", \"sim\"]";

        public const string TYPE_CONFIG_T1 = "[{\"name\":\"字词\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语法\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语义\", \"level\":4, \"suggestion_must\":true, \"suggestion_custom\":\"疑似语义错误，暂时无法给出修改意见。\"}, {\"name\":\"敏感\", \"level\":1, \"suggestion_must\":false, \"suggestion_custom\":\"\"}, {\"name\":\"常识\", \"level\":4, \"suggestion_must\":true}]";

        public const string TYPE_CONFIG_BASE = "[{\"name\":\"字词\", \"level\":1, \"suggestion_must\":true}, {\"name\":\"语法\", \"level\":1, \"suggestion_must\":true}, {\"name\":\"语义\", \"level\":1, \"suggestion_must\":true, \"suggestion_custom\":\"疑似语义错误，暂时无法给出修改意见。\"}, {\"name\":\"敏感\", \"level\":1, \"suggestion_must\":false, \"suggestion_custom\":\"\"}, {\"name\":\"常识\", \"level\":1, \"suggestion_must\":true}]";

        public const string TYPE_CONFIG_YY = "[{\"name\":\"字词\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语法\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语义\", \"level\":2, \"suggestion_must\":true, \"suggestion_custom\":\"\"}, {\"name\":\"敏感\", \"level\":4, \"suggestion_must\":false, \"suggestion_custom\":\"\"}, {\"name\":\"常识\", \"level\":4, \"suggestion_must\":true}]";

        public const string TYPE_CONFIG_CS = "[{\"name\":\"字词\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语法\", \"level\":4, \"suggestion_must\":true}, {\"name\":\"语义\", \"level\":4, \"suggestion_must\":true, \"suggestion_custom\":\"疑似语义错误，暂时无法给出修改意见。\"}, {\"name\":\"敏感\", \"level\":4, \"suggestion_must\":false, \"suggestion_custom\":\"\"}, {\"name\":\"常识\", \"level\":2, \"suggestion_must\":true}]";
    }
}

using System.ComponentModel;

namespace NETCore_Selenium.Domain.Enums
{
    public enum Environment
    {
        [Description("FB1")]
        FB1,
        [Description("FB2")]
        FB2,
        [Description("FB3")]
        FB3,
        [Description("FB4")]
        FB4,
        [Description("FB5")]
        FB5,
        [Description("Test")]
        Test,
        [Description("Stage")]
        Stage,
        [Description("Prod_Gold")]
        Production_Gold,
        [Description("Prod")]
        Production
    }
}
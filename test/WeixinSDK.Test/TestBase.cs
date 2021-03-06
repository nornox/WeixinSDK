﻿using WeixinSDK.Test.Fake;
using Wlitsoft.Framework.Common;
using Wlitsoft.Framework.Common.Logger.Log4Net;
using Wlitsoft.Framework.WeixinSDK;
using Wlitsoft.Framework.WeixinSDK.Config.Extension;
using Wlitsoft.Framework.WeixinSDK.Configuration;
using Wlitsoft.Framework.WeixinSDK.Core;
using Wlitsoft.Framework.WeixinSDK.Extension;
using Wlitsoft.Framework.WeixinSDK.TokenService;
using Xunit;

namespace WeixinSDK.Test
{
    public class TestBase
    {
        /// <summary>
        /// 获取 测试 OpenId。
        /// </summary>
        public string TestOpenId => "ojYGMuO_gEKkFeb9csBPIdPFKqkM";

        static TestBase()
        {
            //设置日志。
            App.Builder.SetLog4NetLoggerByXmlConfig("./Conf/log4net.conf");

            GeneralTokenService tokenService = new GeneralTokenService();

            //设置微信SDK相关。
            App.Builder
                .SetWeixinLogger("WeixinSDKLog")
                .SetWeixinDevConfigByAppSettings()
                .SetWeixinTokenService(tokenService);

            MessageProcessConfiguration pc = new MessageProcessConfiguration();
            pc.MessageList.Add(new MessageConfiguration<RequestTextMessageProcessFake>(RequestMsgType.Text));

            pc.EventMessageList.Add(new EventMessageConfiguration<RequestSubscribeEventMessageKey_001ProcessFake>(RequestMsgEventType.Subscribe));

            App.Builder
                .SetWeixinMessageConfig(pc);

            tokenService.Run();
        }

        [Fact]
        public void InitConfigTesst()
        {
            DevConfiguration devConfig = WeixinApp.DevConfig;

            Assert.NotNull(devConfig);
            Assert.True(devConfig.AppID.Length > 0);
            Assert.True(devConfig.AppSecret.Length > 0);
        }
    }
}

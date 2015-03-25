﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Card;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class CardTest : CommonApiTest
    {
        protected Card_BaseInfoBase _BaseInfo = new Card_BaseInfoBase()
            {
                logo_url = "http:\\www.supadmin.cn/uploads/allimg/120216/1_120216214725_1.jpg",
                brand_name = "海底捞",
                code_type = Card_CodeType.CODE_TYPE_TEXT,
                title = "132 元双人火锅套餐",
                sub_title = "",
                color = "Color010",
                notice = "使用时向服务员出示此券",
                service_phone = "020-88888888",
                description = @"不可与其他优惠同享\n 如需团购券发票，请在消费时向商户提出\n 店内均可
使用，仅限堂食\n 餐前不可打包，餐后未吃完，可打包\n 本团购券不限人数，建议2 人使用，超过建议人
数须另收酱料费5 元/位\n 本单谢绝自带酒水饮料",
                date_info = new Card_BaseInfo_DateInfo()
                {
                    type = 1,
                    begin_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now),
                    end_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now.AddDays(10)),
                },
                sku = new Card_BaseInfo_Sku()
                {
                    quantity = 5
                },
                use_limit = 1,
                get_limit = 3,
                use_custom_code = false,
                bind_openid = false,
                can_share = true,
                can_give_friend = true,
                url_name_type = Card_UrlNameType.URL_NAME_TYPE_RESERVATION,
                custom_url = "http://www.weiweihi.com",
                source = "大众点评"
            };

        [TestMethod]
        public void CreateCardTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var data = new Card_GrouponData()
                {
                    base_info = _BaseInfo,
                    deal_detail = "测试"
                };

            var result = CardApi.CreateCard(accessToken, data);
            Console.Write(result);
            Assert.IsNotNull(result);

            var data3 = new Card_CashData()
            {
                base_info = _BaseInfo,
                least_cost = 11,
                reduce_cost = 11
            };
            var result3 = CardApi.CreateCard(accessToken, data3);
            Console.Write(result3);
            Assert.IsNotNull(result3);

            var data2 = new Card_MeetingTicketData()
                {
                    base_info = _BaseInfo,
                    //map_url = "http://localhost:18666/images/v2/logo%20.png",
                    meeting_detail = "测试asdsasdsasdsa"
                };
            var result2 = CardApi.CreateCard(accessToken, data2);
            Console.Write(result2);
            Assert.IsNotNull(result2);
        }

        //[TestMethod]
        public List<string> CardBatchGetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardApi.CardBatchGet(accessToken, 0, 5);
            Console.Write(result);
            Assert.IsNotNull(result);
            return result.card_id_list;
        }

        [TestMethod]
        public void CreateQRTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var cardIdList = CardBatchGetTest();
            var cardId = cardIdList.FirstOrDefault();

            var result = CardApi.CreateQR(accessToken, cardId);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetColorsTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardApi.GetColors(accessToken);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CardDetailGet()
        {
            string cardId = "p3IHxjt-CLCTd_r3eZ9cQqM7jrZE";    //换成你的卡券Id

            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardApi.CardDetailGet(accessToken, cardId);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        protected Store_Location _StoreLocation = new Store_Location()
        {
            business_name = "TIT 创意园1 号店",
            province = "广东省",
            city = "广州市",
            district = "海珠区",
            address = "中国广东省广州市海珠区艺苑路11 号",
            telephone = "020-89772059",
            category = "房产小区",
            longitude = "115.32375",
            latitude = "25.097486"
        };

        [TestMethod]
        public void StoreBatchAddTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var data = new StoreLocationData()
            {
                location_list = new List<Store_Location>()
                        {
                            _StoreLocation
                        },
            };

            var result = CardApi.StoreBatchAdd(accessToken, data);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void BatchGetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardApi.BatchGet(accessToken, 0, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UploadLogoTest()
        {
            string file = @"E:\1.jpg";

            var accessToken = AccessTokenContainer.GetToken(_appId);
            var result = CardApi.UploadLogo(accessToken, file);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}

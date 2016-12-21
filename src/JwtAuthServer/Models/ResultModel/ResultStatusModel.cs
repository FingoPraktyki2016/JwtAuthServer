﻿using LegnicaIT.JwtAuthServer.ResultPattern;

namespace LegnicaIT.JwtAuthServer.Models.ResultModel
{
    public class ResultStatusModel
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}

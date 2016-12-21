﻿using LegnicaIT.JwtAuthServer.GenericResult;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class ResultModel<T>
    {
        public T Value { get; set; }
        public ResultStatusModel Status { get; set; }

        public ResultModel(T value, ResultCode code = ResultCode.Ok)
        {
            Value = value;
            Status = new ResultStatusModel() { Code = code };
        }
    }
}

﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudDT.Shared.Services
{
    public class ContainerService
    {
        private readonly string api = "https://cloudshell.conchbrain.club";

        private readonly HttpClient httpClient = new();

        private string containerId = string.Empty;

        public List<int> Ports { get; } = new();

        /// <summary>
        /// 创建环境
        /// </summary>
        public async Task<bool> Create()
        {
            if (!string.IsNullOrEmpty(containerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/create?clouddt");
            containerId = await responseMessage.Content.ReadAsStringAsync();
            return responseMessage.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// 关闭环境
        /// </summary>
        public async Task<bool> Kill()
        {
            if (string.IsNullOrEmpty(containerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/kill?{containerId}");
            containerId = await responseMessage.Content.ReadAsStringAsync();
            bool flag = responseMessage.StatusCode == HttpStatusCode.OK;

            if (flag)
                containerId = string.Empty;

            return flag;
        }

        /// <summary>
        /// 转发端口
        /// </summary>
        /// <param name="port">端口号</param>
        public async Task<bool> ForwardPort(int port)
        {
            if (string.IsNullOrEmpty(containerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/forward?id=${containerId}&port=${port}");
            bool flag = responseMessage.StatusCode == HttpStatusCode.OK;

            if (flag)
                Ports.Add(port);

            return flag;
        }

    }
}
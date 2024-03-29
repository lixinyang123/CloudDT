﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CloudDT.Shared.Services
{
    public class ContainerService
    {
        private readonly string api = "https://cloudshell.conchbrain.club";

        private readonly HttpClient httpClient = new();

        public string ContainerId { get; set; } = string.Empty;

        public Dictionary<int, string> Ports { get; } = new();

        public string TTYHref { get => $"{api}/{ContainerId}/"; }

        /// <summary>
        /// 创建环境
        /// </summary>
        public async Task<bool> Create()
        {
            if (!string.IsNullOrEmpty(ContainerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/create?clouddt");
            bool flag = responseMessage.StatusCode == HttpStatusCode.OK;

            if (flag)
            {
                ContainerId = await responseMessage.Content.ReadAsStringAsync();
                Ports.Add(7681, $"{api}/forward/{ContainerId}/{7681}");
                Console.WriteLine(ContainerId);
            }

            return flag;
        }

        /// <summary>
        /// 关闭环境
        /// </summary>
        public async Task<bool> Kill()
        {
            if (string.IsNullOrEmpty(ContainerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/kill?{ContainerId}");
            bool flag = responseMessage.StatusCode == HttpStatusCode.OK;

            if (flag)
                ContainerId = string.Empty;

            return flag;
        }

        /// <summary>
        /// 转发端口
        /// </summary>
        /// <param name="port">端口号</param>
        public async Task<bool> ForwardPort(int port)
        {
            if (string.IsNullOrEmpty(ContainerId))
                return false;

            HttpResponseMessage responseMessage = await httpClient!.GetAsync($"{api}/forward?id={ContainerId}&port={port}");
            bool flag = responseMessage.StatusCode == HttpStatusCode.OK;

            if (flag && !Ports.ContainsKey(port))
                Ports.Add(port, $"{api}/forward/{ContainerId}/{port}");

            return flag;
        }
    }
}

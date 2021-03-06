﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;

namespace MvcFXProductMgr.Controllers
{
    public class TestingOrgController : Controller
    {
        /// <summary>
        /// 获取信息的列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            List<TestingOrgModel> list = new List<TestingOrgModel>();
            TestingOrgModel model = new TestingOrgModel();
            list = model.GetAllTestingOrgs();
            return View(list);
        }
        //

        /// <summary>
        /// 创建公司信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestingOrgManage(int id = 0)
        {         
            TestingOrgModel model = new TestingOrgModel();
            if (id == 0)
            {               
                model.Id = 0;
                model.Name = "";
                model.Url = "";
                model.Tel = "";
            }
            else
            {
                model = model.GetTestingOrgById(id);
            }
            return View(model);
        }

        /// <summary>
        /// 增加或修改检测机构信息
        /// id=0时，新增检测机构信息
        /// id>0时，修改检测机构信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestingOrgManage([Bind(Include = "Id,Name,Url,Tel")]TestingOrgModel model)
        {
            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;

            if (model.Id == 0)
            {
                model.AddTestingOrg(model);

                logModel.Content = "AddTestingOrg";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }
                HttpContext.Response.Write("<script>alert('保存成功！')</script>");
                return View("TestingOrgManage");               
            }
            else
            {
                model.UpdateTestingOrg(model);
                logModel.Content = "UpdateTestingOrg";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }
                return RedirectToAction("Index", "TestingOrg");
            }
        }

        /// <summary>
        /// 删除检测机构
        /// </summary>
        /// <returns></returns>

        public ActionResult DeleteTestingOrg(int id)
        {
            TestingOrgModel model = new TestingOrgModel();
            model.DeleteTestingOrg(id);

            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;
            logModel.Content = "DeleteTestingOrg";
            try
            {
                logModel.AddLog(logModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "日志创建失败");
            }

            return RedirectToAction("Index", "TestingOrg");

        }


    }
}

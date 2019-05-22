using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace WEB.BaseApplication
{
    public abstract class BootstrapWebViewPage<T> : System.Web.Mvc.WebViewPage<T>
    {
        //在cshtml页面里面使用的变量
        public BootstrapHelper Bootstrap { get; set; }

        /// <summary>
        /// 初始化Bootstrap对象
        /// </summary>
        public override void InitHelpers()
        {
            base.InitHelpers();
            Bootstrap = new BootstrapHelper(ViewContext, this);
        }

        public override void Execute()
        {
            //throw new NotImplementedException();
        }
    }
    public class BootstrapHelper : System.Web.Mvc.HtmlHelper
    {
        /// <summary>
        /// 使用指定的视图上下文和视图数据容器来初始化 BootstrapHelper 类的新实例。
        /// </summary>
        /// <param name="viewContext">视图上下文</param>
        /// <param name="viewDataContainer">视图数据容器</param>
        public BootstrapHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : base(viewContext, viewDataContainer)
        { }

        /// <summary>
        /// 使用指定的视图上下文、视图数据容器和路由集合来初始化 BootstrapHelper 类的新实例。
        /// </summary>
        /// <param name="viewContext">视图上下文</param>
        /// <param name="viewDataContainer">视图数据容器</param>
        /// <param name="routeCollection">路由集合</param>
        public BootstrapHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        { }
    }
    public static class LabelExtensions
    {
        /// <summary>
        /// 通过使用指定的 HTML 帮助器和窗体字段的名称，返回Label标签
        /// </summary>
        /// <param name="html">扩展方法实例</param>
        /// <param name="id">标签的id</param>
        /// <param name="content">标签的内容</param>
        /// <param name="cssClass">标签的class样式</param>
        /// <param name="htmlAttributes">标签的额外属性（如果属性里面含有“-”，请用“_”代替）</param>
        /// <returns>label标签的html字符串</returns>
        public static MvcHtmlString Label(this BootstrapHelper html, string id, string content, string cssClass, object htmlAttributes)
        {
            //定义标签的名称
            TagBuilder tag = new TagBuilder("label");
            //给标签增加额外的属性
            IDictionary<string, object> attributes = BootstrapHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!string.IsNullOrEmpty(id))
            {
                attributes.Add("id", id);
            }
            if (!string.IsNullOrEmpty(cssClass))
            {
                //给标签增加样式
                tag.AddCssClass(cssClass);
            }
            //给标签增加文本
            tag.SetInnerText(content);
            tag.AddCssClass("control-label");
            tag.MergeAttributes(attributes);
            return MvcHtmlString.Create(tag.ToString());
        }
    }
    public static class HtmlHelperExtend
    {
        public static MvcHtmlString LabelFor(this HtmlHelper html, string lableName, string labelFor)
        {
            return MvcHtmlString.Create(string.Format("<label for=\"{1}\" style=\"white-space:nowrap;\">{0}</label>", lableName, labelFor));
        }

        /// <summary>
        /// 操作成功时显示信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg">显示的信息</param>
        /// <returns></returns>
        public static MvcHtmlString SuccessText(this HtmlHelper html, string msg)
        {
            return MvcHtmlString.Create(string.Format("<div style='color:green;margin-left:5px;'>{0}</div>", msg));
        }
        /// <summary>
        /// 操作成功时显示信息，把信息传入ViewData["success"]
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SuccessText(this HtmlHelper html)
        {
            if (html.ViewData["success"] == null)
            {
                return MvcHtmlString.Empty;
            }
            return SuccessText(html, html.ViewData["success"].ToString());
        }

        /// <summary>
        /// 出错时显示信息
        /// </summary>
        /// <param name="html"></param>
        /// <param name="msg">显示的信息</param>
        /// <returns></returns>
        public static MvcHtmlString ErrorText(this HtmlHelper html, string msg)
        {
            return MvcHtmlString.Create(string.Format("<div style='font-size:14px;color:red;margin-left:5px;'>{0}</div>", msg));
        }
        /// <summary>
        /// 操作成功时显示信息，把信息传入ViewData["error"]
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString ErrorText(this HtmlHelper html)
        {
            if (html.ViewData["error"] == null)
            {
                return MvcHtmlString.Empty;
            }
            return ErrorText(html, html.ViewData["error"].ToString());
        }

        /* BEGIN button */
        #region button
        public static MvcHtmlString Button(this HtmlHelper html, string value, string onclickScript = null, string rightNO = null, string parameters = null)
        {
            if (string.IsNullOrWhiteSpace(rightNO) || BLL.ServiceHelper.AllowedAccess(rightNO))
            {
                string result = "<button  type=\"button\"";
                if (!string.IsNullOrEmpty(onclickScript)) result += string.Format(" onclick=\"{0}\" ", onclickScript);
                if (!string.IsNullOrEmpty(parameters)) result += parameters;
                result += string.Format(" >{0}</button>", value);
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Empty;
        }

        //库存单 明细 审核
        public static MvcHtmlString Button_StockChange_Approval(this HtmlHelper html, string url, string rightNO = null, string btnText = null, string alertMsg = null, string parameters = null)
        {
            if (string.IsNullOrEmpty(btnText)) btnText = Msg.Approval;
            if (string.IsNullOrEmpty(alertMsg)) alertMsg = Msg.Approval_Confirm;
            string script = "showConfirmMsg('" + alertMsg + "', function () { $(':button[value=" + btnText + "]').hide(); $.post('" + url + "', function (data) { if (data.success) { window.location.reload(); } else { showErrorMsg(data.message); } }); }); ";
            return Button(html, btnText, script, rightNO, parameters);
        }
        public static MvcHtmlString Button_StockChange_Revoke(this HtmlHelper html, string url, string rightNO = null, string btnText = null, string alertMsg = null, string parameters = null)
        {
            if (string.IsNullOrEmpty(btnText)) btnText = Msg.Revoke;
            if (string.IsNullOrEmpty(alertMsg)) alertMsg = Msg.ApprovalCancel_Confirm;
            string script = "showConfirmMsg('" + alertMsg + "', function () { $(':button[value=" + btnText + "]').hide(); $.post('" + url + "', function (data) { if (data.success) { window.location.reload(); } else { showErrorMsg(data.message); } }); }); ";
            return Button(html, btnText, script, rightNO, parameters);
        }
        public static MvcHtmlString Button_StockChange_Delete(this HtmlHelper html, string url, string rightNO = null, string parameters = null)
        {
            string script = "showConfirmMsg('" + Msg.Delete_Confirm + "', function () { $.post('" + url + "', function (data) { if (data.success){ parent.$.fancybox.close(); parent.reloadGrid(); } else { showErrorMsg(data.message); } }); });";
            return Button(html, Msg.Delete, script, rightNO, parameters);
        }
        public static MvcHtmlString Button_StockChange_Edit(this HtmlHelper html, string url, string rightNO = null, string parameters = null)
        {
            string script = "window.location.href ='" + url + "';";
            return Button(html, Msg.Edit, script, rightNO, parameters);
        }

        public static MvcHtmlString ButtonForGridSearch(this HtmlHelper html, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<button type=\"button\" onclick=\"gridSearch($(this).parent(),'{0}'); \">" + Msg.Search + "</button>", gridID));
        }
        public static MvcHtmlString ButtonForGridSearchMore(this HtmlHelper html)
        {
            return MvcHtmlString.Create("<input  type=\"button\" value=\"更多\" onclick=\" $('[sdm=more]', '.bodySearch').toggle();\"  />");
        }

        /// <summary>
        /// 带搜索框和搜索按钮
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchButtonForGrid(this HtmlHelper html, string gridID = "gridList", string parameters = "", string placeholder = "")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"search\" " + parameters + "onBlur=\"" + "this.value=this.value.replace(/\\s/gi,'')\"" + "   placeholder=\"" + placeholder + "\"onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />" + ButtonForGridSearch(html, gridID));
        }
        /// <summary>
        /// 带搜索框和搜索按钮、带详细搜索按钮
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchButtonMoreForGrid(this HtmlHelper html, string gridID = "gridList", string parameters = "", string placeholder = "")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"search\" " + parameters + "   placeholder=\"" + placeholder + "\"onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />" + ButtonForGridSearch(html, gridID) + ButtonForGridSearchMore(html));
        }
        /// <summary>
        /// 创建搜索框
        /// </summary>
        /// <param name="html"></param>
        /// <param name="textName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MvcHtmlString SearchTextForGrid(this HtmlHelper html, string textName, string parameters = "", string textWidth = "200px")
        {
            return MvcHtmlString.Create("<input type=\"text\" name=\"" + textName + "\" placeholder=\"" + parameters + "\" style=\"width:" + textWidth + "\" onkeypress=\"if(event.keyCode==13) {$(this).siblings('button:eq(0)').click();}\" />");
        }
        public static MvcHtmlString CloseFormButton(this HtmlHelper html, string value)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"$(this).parents('form').hide();\" />", value));
        }

        public static MvcHtmlString SubmitGrid(this HtmlHelper html, string value, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<button type=\"button\" onclick=\"gridSearch($(this).parent(),'{0}'); \">" + value + "</button>", gridID));
        }

        public static MvcHtmlString Submit(this HtmlHelper html, string value, string parameters)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"submit\" value=\"{0}\" {1}/>", value, parameters));
        }
        public static MvcHtmlString Submit(this HtmlHelper html, string value, bool clickToHide = false)
        {
            return Submit(html, value, clickToHide ? "onclick=' $(this).hide();'" : "");
        }


        /// <summary>
        /// submit 添加
        /// </summary>
        /// <param name="html"></param>
        /// <param name="clickToHide"></param>
        /// <param name="text">显示文字</param>
        /// <returns></returns>
        public static MvcHtmlString Submit_Add(this HtmlHelper html, bool clickToHide = false, string text = "")
        {
            var displayText = Msg.Btn_Add;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayText = text;
            }
            //edit:17/5/4 yly 添加的时候弹出等待提示
            return Submit(html, displayText, clickToHide ? "onclick=' $(this).hide();'" : "onclick=\"if($('form').valid()){showWaitingMsg();}\"");
        }
        //submit 保存
        public static MvcHtmlString Submit_Save(this HtmlHelper html, bool clickToHide = false)
        {
            return Submit(html, Msg.Btn_Save, clickToHide ? "onclick=' $(this).hide();'" : "");
        }

        public static MvcHtmlString SubmitDefault(this HtmlHelper html, string value)
        {
            return Submit(html, value, "class=\"button default\"");
        }

        public static MvcHtmlString ResetButton(this HtmlHelper html, string value, string parameters = "")
        {
            return MvcHtmlString.Create(string.Format("<input type=\"reset\" value=\"{0}\" {1}/>", value, parameters));
        }

        #endregion
        /* END button */

        /* BEGIN fancybox */
        #region fancybox
        public static MvcHtmlString FB_Close(this HtmlHelper html, string url = null)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"parent.$.fancybox.close()||window.close();" + (string.IsNullOrEmpty(url) ? "" : "self.parent.openPage('" + url + "');") + "\"/> ", "关闭"));
        }
        public static MvcHtmlString Window_Close(this HtmlHelper html, string url = null)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"javascript:window.opener=null;window.close();\"/> ", "关闭"));
        }

        public static MvcHtmlString FB_CloseToGrid(this HtmlHelper html, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"parent.$.fancybox.close();self.parent.reloadJqGrid('{0}');\"/> ", Msg.Btn_Close, gridID));
        }
        /// <summary>
        /// 关闭后执行js
        /// </summary>
        /// <param name="html"></param>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public static MvcHtmlString FB_CloseToCode(this HtmlHelper html, string jsCode)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"parent.$.fancybox.close();{1}\"/> ", Msg.Btn_Close, jsCode));
        }

        public static MvcHtmlString FB_Button(this HtmlHelper html, string name, string value, string url, int width, int height)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" name=\"{0}\" "
                               + " value=\"{1}\" "
                               + " onclick=\"showPage('{2}','{3}','{4}');\" /> ",
                               name, value, url, width, height
                               ));
        }
        public static MvcHtmlString FB_GridCheckBoxAll(this HtmlHelper html)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"checkbox\" id=\"chkAll\" name=\"chkAll\" value=\"checkbox\" onclick=\"checkAll('chkAll',this);\" />"));
        }
        public static MvcHtmlString FB_GridCheckbox(this HtmlHelper html, string id)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"checkbox\" name=\"chkSelect\" value=\"{0}\" onclick=\"checkAll('chkAll',this);\" />", id));
        }
        public static MvcHtmlString FB_GridCheckbox(this HtmlHelper html, int id)
        {
            return FB_GridCheckbox(html, id.ToString());
        }
        #endregion
        /* END fancybox */

        /* BEGIN jqGrid */
        #region jqGrid
        public static MvcHtmlString JG_ListAndPager(this HtmlHelper cntrl, string gridID = "gridList", string pagerID = "gridPager")
        {
            return MvcHtmlString.Create(JG_List(cntrl, gridID).ToString() + JG_Pager(cntrl, pagerID).ToString());
        }
        public static MvcHtmlString JG_List(this HtmlHelper cntrl, string gridID = "gridList")
        {
            return MvcHtmlString.Create(string.Format("<table id=\"{0}\" style=\"margin-bottom:2px;\"></table>", gridID));
        }
        public static MvcHtmlString JG_Pager(this HtmlHelper cntrl, string pagerID = "gridPager")
        {
            return MvcHtmlString.Create(string.Format("<div id=\"{0}\"></div>", pagerID));
        }

        public static MvcHtmlString JG_TextBoxSearch(this HtmlHelper cntrl)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"text\" name=\"search\" value=\"\" class=\"filter\" />"));
        }

        public static MvcHtmlString XP_TextBox(string id, string name)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"text\" name=\"" + name + "\" id=\"" + id + "\" value=\"\" />"));
        }




        public static MvcHtmlString JG_Close(this HtmlHelper cntrl, string gridID = "gridList", string extParam = "")
        {
            string result = "";
            result += "self.parent.tb_remove();";
            result += string.Format("self.parent.reloadJqGrid('{0}');", gridID);
            result += extParam;
            result = string.Format(
                "<input type=\"button\" value=\"{0}\" onclick=\"{1}\"/>",
                "确定", result);
            return MvcHtmlString.Create(result);
        }
        public static MvcHtmlString JG_ButtonSeparator(this HtmlHelper cntrl)
        {
            return MvcHtmlString.Create(string.Format("<span style='border-right:1px solid #ccc; margin:1px; '></span>"));
        }
        #endregion
        /* END jqGrid */

        /*bodyButton*/
        #region bodyButton
        /// <summary>
        /// 生成bodyButton里按钮的标签
        /// </summary>      
        /// <param name="text">显示在按钮上的文字</param>
        /// <param name="onclickScript">点击后要执行的脚本</param>
        /// <param name="rightNO">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="name">按钮的id和name</param>
        /// <param name="parameters">其它属性</param>
        public static MvcHtmlString BodyButton(this HtmlHelper html, string text, string onclickScript, string rightNO = null, string className = "default", string name = null, string parameters = null, string title = null)
        {
            if (string.IsNullOrWhiteSpace(rightNO) || BLL.ServiceHelper.AllowedAccess(rightNO))
            {
                string result = "<a ";
                if (!string.IsNullOrEmpty(name)) result += string.Format("id=\"{0}\" name=\"{0}\" ", name);
                if (!string.IsNullOrEmpty(className)) result += string.Format("class=\"{0}\" ", className);
                if (!string.IsNullOrEmpty(onclickScript)) result += string.Format("onclick=\"{0}\" ", onclickScript);
                if (!string.IsNullOrEmpty(parameters)) result += parameters;
                if (!string.IsNullOrEmpty(title)) result += string.Format("title=\"{0}\" ", title);
                result += string.Format(" >{0}</a>", text);
                return MvcHtmlString.Create(result);
            }
            return MvcHtmlString.Empty;
        }
        /// <summary>
        /// 生成bodyButton里添加按钮的标签
        /// </summary>
        /// <param name="url">添加页面的路径</param>
        /// <param name="width">新开的的页面宽度</param>
        /// <param name="height">新开的的页面高度</param>
        /// <param name="rightNO">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="text">显示在按钮上的文字:默认添加</param>
        public static MvcHtmlString BodyButton_Add(this HtmlHelper html, string url, object width, object height, string rightNO = null, string className = "add", string text = "")
        {
            if (className == null) className = "add";
            string script = string.Format("showPage('{0}','{1}', '{2}');", url, width, height);
            var displayName = Msg.Add;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayName = text;
            }
            return BodyButton(html, displayName, script, rightNO, className);
            /*
             showPage('" + Url.Action("Add") + "','400', '300');
             */
        }
        /// <summary>
        /// 生成bodyButton里编辑按钮的标签，用于勾选Grid单行后编辑此行
        /// </summary>
        /// <param name="url">编辑页面的路径，并附带参数名如"id="或"/"，后面会自动加上获取的ID值</param>
        /// <param name="width">新开的的页面宽度</param>
        /// <param name="height">新开的的页面高度</param>
        /// <param name="rightNO">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="gridID">获取id的Grid ID</param>
        /// <param name="text">显示在按钮上的文字:默认添加</param>
        public static MvcHtmlString BodyButton_Edit(this HtmlHelper html, string url, object width, object height, string rightNO = null, string className = "edit", string gridID = "gridList", string text = "")
        {
            if (className == null) className = "edit";
            var displayName = Msg.Edit;
            if (!string.IsNullOrWhiteSpace(text))
            {
                displayName = text;
            }
            string script = "var id = $('#" + gridID + "').getGridParam('selarrrow'); if (id.length != 1) { showAlertMsg('" + Msg.Message_OnlyAllowOneItemSelect + "'); return } showPage('" + url + "' + id, '" + (string)width + "', '" + (string)height + "');";
            return BodyButton(html, displayName, script, rightNO, className);
            /*
             function edit() {
                var id = $("#gridList").getGridParam('selarrrow');
                if (id.length != 1) { showAlertMsg("@Msg.Message_OnlyAllowOneItemSelect"); return }
                showPage('@Url.Action("Edit")/' + id, '400', '280');
            }
             */
        }

        /// <summary>
        /// 生成bodyButton里删除按钮的标签，用于勾选Grid多行后删除这些行数据
        /// </summary>
        /// <param name="url">删除页面的路径，并附带参数名如"ids="或"/"，后面会自动加上获取的ID值(多ID用英文逗号分隔)</param>
        /// <param name="rightNO">权限编号(可根据权限编号判断是否显示此按钮)</param>
        /// <param name="className">样式名</param>
        /// <param name="gridID">获取ids的Grid ID</param>
        /// <returns></returns>
        public static MvcHtmlString BodyButton_Del(this HtmlHelper html, string url, string rightNO = null, string className = "delete", string gridID = "gridList", string customName = "")
        {
            if (className == null) className = "delete";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + Msg.Delete_Confirm + "', function () { var dialogObj = showWaitingMsg();  $.post('" + url + "' + ids, function (data) {  dialogObj.dialog('destroy'); if (data.success){ reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); setTimeout(function(){$('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid');},0); } } else { showErrorMsg(data.message); } }); });";
            if (string.IsNullOrEmpty(customName))
            {
                customName = Msg.Delete;
            }

            return BodyButton(html, customName, script, rightNO, className);
            /*
            function del() {
                var ids = $("#gridList").getGridParam('selarrrow');
                if (ids.length <= 0) { showAlertMsg('@Msg.Message_NullSelect'); return false; }
                showConfirmMsg("@Msg.Delete_Confirm", function () {
                    $.post('@Url.Action("Delete")?ids=' + ids, function (data) {                       
                        if (data.success) {
                            reloadGrid();
                            // reloadJqGrid('gridList');
                        }
                        else {
                            showErrorMsg(data.message);
                        }
                    });
                });
            }
             */
        }

        //审核
        public static MvcHtmlString BodyButton_Approval(this HtmlHelper html, string url, string rightNO = null, string className = "audit", string gridID = "gridList")
        {
            if (className == null) className = "audit";
            string script = " var displayset = this; var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) {  showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + Msg.Approval_Confirm + "', function () { displayset.style.display ='none'; var dialogObj = showWaitingMsg(); $.post('" + url + "' + ids, function (data) { dialogObj.dialog('destroy'); if (data.success){  reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); $('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid'); } } else {showErrorMsg(data.message); }displayset.style.display = 'inline'; }); });";
            return BodyButton(html, Msg.Approval, script, rightNO, className);
        }
        //撤销审核
        public static MvcHtmlString BodyButton_Revoke(this HtmlHelper html, string url, string rightNO = null, string className = "revoke", string gridID = "gridList", string btnText = null, string alertMsg = null)
        {
            if (className == null) className = "revoke";
            if (string.IsNullOrEmpty(gridID)) gridID = "gridList";
            if (string.IsNullOrEmpty(btnText)) btnText = Msg.Revoke;
            if (string.IsNullOrEmpty(alertMsg)) alertMsg = Msg.ApprovalCancel_Confirm;
            string script = " var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } showConfirmMsg('" + alertMsg + "', function () {var dialogObj = showWaitingMsg();  $.post('" + url + "' + ids, function (data) {dialogObj.dialog('destroy');  if (data.success){ reloadJqGrid('" + gridID + "'); if('" + gridID + "'=='gridList'){ $('#itemTitle').text(''); $('#gridList2').jqGrid('setGridParam', { postData: null, page: 1 }).trigger('reloadGrid'); } } else { showErrorMsg(data.message); }  }); });";
            return BodyButton(html, btnText, script, rightNO, className);
        }

        public static MvcHtmlString BodyButton_Print(this HtmlHelper html, string url, string rightNO = null, string className = "print", string gridID = "gridList")
        {
            if (className == null) className = "print";
            if (string.IsNullOrWhiteSpace(gridID)) gridID = "gridList";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showAlertMsg('" + Msg.Message_NullSelect + "'); return false; } window.open('" + url + "'+ ids, '', 'width=649,height=800, top=0, left=0, toolbar=no, menubar=no, resizable=yes,location=no, status=no');";
            return BodyButton(html, Msg.Print, script, rightNO, className);
        }
        //导出
        public static MvcHtmlString BodyButton_Export(this HtmlHelper html, string url, string rightNO = null, string className = "exportExcel", string gridID = "gridList", string hidFrameID = "submitZone")
        {
            if (className == null) className = "exportExcel";
            if (hidFrameID == null) className = "submitZone";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showConfirmMsg('" + Msg.Export_Confirm + "', function () {  $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }); }else { $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }";//合并(适用于加密导出)
            return BodyButton(html, Msg.Export, script, rightNO, className);
        }
        /// <summary>
        /// 自定义名称导出
        /// </summary>
        /// <param name="html"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="rightNO"></param>
        /// <param name="className"></param>
        /// <param name="gridID"></param>
        /// <param name="hidFrameID"></param>
        /// 注意导出名称不易过长（因为是get请求）
        /// <returns></returns>
        public static MvcHtmlString BodyButton_Exports(this HtmlHelper html, string url, string name = "Export", string rightNO = null, string className = "exportExcel", string gridID = "gridList", string hidFrameID = "submitZone")
        {
            if (className == null) className = "exportExcel";
            if (hidFrameID == null) className = "submitZone";
            string script = "var ids = $('#" + gridID + "').getGridParam('selarrrow'); if (ids.length <= 0) { showConfirmMsg('" + Msg.Export_Confirm + "', function () {  $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }); }else { $('#" + hidFrameID + "').attr('src', exportSearch('" + url + "' + ids)); }";//合并(适用于加密导出)
            return BodyButton(html, name, script, rightNO, className);
        }
        #endregion
        /*bodyButton*/

        public static MvcHtmlString CloseWindowButton(this HtmlHelper html)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"window.close();\" ", Msg.Btn_Close));
        }
        public static MvcHtmlString PopupButton(this HtmlHelper html, string value, string url, string target)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"window.open('{1}','pop')\" />", value, url));
        }
        public static MvcHtmlString RedirectButton(this HtmlHelper html, string value, string url)
        {
            return MvcHtmlString.Create(string.Format("<input type=\"button\" value=\"{0}\" onclick=\"location.href='{1}';\" />", value, url));
        }


        public static string StripQuote(this HtmlHelper html, string value)
        {
            string result = value;

            result = result.Replace("'", "");
            result = result.Replace("\"", "");
            return result;
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            IEnumerable<string> radioButtons = selectList.Select<SelectListItem, string>(
                item => "<label class='inline'>" + html.RadioButton(name, item.Value, item.Selected, htmlAttributes) + item.Text + "</label>"
                );
            return MvcHtmlString.Create(string.Join("", radioButtons.ToArray()));
        }
        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name, object htmlAttributes)
        {
            return RadioButtonList(html, name, (IEnumerable<SelectListItem>)html.ViewData[name], htmlAttributes);
        }
        public static MvcHtmlString RadioButtonList(this HtmlHelper html, string name)
        {
            return RadioButtonList(html, name, null);
        }


        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            int i = 1;
            IEnumerable<string> radioButtons = selectList.Select<SelectListItem, string>(
                item => html.CheckBox(item.Text, name, name + (i++).ToString(), item.Value, item.Selected, htmlAttributes).ToHtmlString()
                );
            return MvcHtmlString.Create(string.Join("", radioButtons.ToArray()));
        }
        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name, object htmlAttributes)
        {
            return CheckboxList(html, name, (IEnumerable<SelectListItem>)html.ViewData[name], htmlAttributes);
        }
        public static MvcHtmlString CheckboxList(this HtmlHelper html, string name)
        {
            return CheckboxList(html, name, null);
        }


        public static string JS_Escape(this HtmlHelper html, string value)
        {
            string result = value.Replace(Environment.NewLine, "");
            result = result.Replace("\\", "\\\\");
            result = result.Replace("'", "&#39;");
            return result;
        }

        public static string JS_Escape(this HtmlHelper html, MvcHtmlString value)
        {
            string result = value.ToHtmlString().Replace(Environment.NewLine, "");
            return result;
        }

        public static MvcHtmlString CheckBox(this HtmlHelper html, string label, string name, string id = null, string value = "True", bool isChecked = false, object htmlAttributes = null)
        {
            if (id == null) id = name;
            StringBuilder sb = new StringBuilder();
            sb.Append("<label for=\"").Append(id).Append("\">");
            sb.Append("<input type=\"checkbox\" id=\"").Append(id).Append("\" alt=\"").Append(label).Append("\" name=\"").Append(name).Append("\" value=\"").Append(value).Append("\" ").Append(isChecked ? "checked=\"checked\" " : " ").Append(HtmlAttributesToString(htmlAttributes)).Append(" />");
            sb.Append(label);
            sb.Append("</label>");
            return MvcHtmlString.Create(sb.ToString());
        }

        private static string HtmlAttributesToString(object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return "";
            }
            else
            {
                string temp = htmlAttributes.ToString();
                if (string.Compare(temp, "System.Object", false) == 0)
                {
                    return "";
                }
                else
                {
                    return temp.Substring(1, temp.Length - 2);
                }
            }
        }

        public static MvcHtmlString HiddenFrame(this HtmlHelper html, string name = "submitZone", string parameters = "")
        {
            //<iframe src='' style='display:none;' name='submitZone' id='submitZone'></iframe>
            return MvcHtmlString.Create(string.Format("<iframe src='' style='display:none;' name='{0}' id='{0}' {1}></iframe>", name, parameters));
        }

        /// <summary>
        /// 选择用户
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="userNum"></param>
        /// <param name="isReadonly"></param>
        /// <param name="style"></param>
        /// <param name="attr"></param>
        /// <param name="deptId">初始化部门</param>
        /// <returns></returns>
        public static MvcHtmlString UserSelectInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, int? deptId = 0, string deptIds = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/User/UserSelect?objID={0}&num={2}&deptId={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, deptId, deptIds));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/User/UserSelect?objID={0}&num={2}&deptId={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, deptId, deptIds));
        }
        public static MvcHtmlString RoleSelectInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, int? roleId = 0, string roleIds = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/User/RoleSelect?objID={0}&num={2}&roleId={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, roleId, roleIds));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/User/RoleSelect?objID={0}&num={2}&roleId={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, roleId, roleIds));
        }


        public static MvcHtmlString RoleNameSelectInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string roleName = "", string roleIds = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/User/RoleNameSelect?objID={0}&num={2}&roleName={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, roleName, roleIds));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/User/RoleNameSelect?objID={0}&num={2}&roleName={6}&ids={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, roleName, roleIds));
        }

        public static MvcHtmlString DepartmentSelectInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/Department/DepartmentSelect?objID={0}&num={2}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Department/DepartmentSelect?objID={0}&num={2}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr));
        }

        public static MvcHtmlString DepartmentSelectInputWithoutLine(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/Department/DepartmentSelectWithoutLine?objID={0}&num={2}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Department/DepartmentSelectWithoutLine?objID={0}&num={2}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr));
        }

        /// <summary>
        /// 选择员工Html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="userNum"></param>
        /// <param name="isReadonly"></param>
        /// <param name="style"></param>
        /// <param name="attr"></param>
        /// <param name="postions">职位级别</param>
        /// <param name="deptId">初始化部门</param>
        /// <returns></returns>
        public static MvcHtmlString StaffSelectInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null, int? deptId = null, int? width = null, int? heigh = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input type='text' id='{0}' name='{0}' value='{1}' onclick=\"showPage('/Staff/StaffSelect?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
            }
            //add by pyl 增加文本域的长和宽属性
            if (width.HasValue && heigh.HasValue)
            {
                return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Staff/StaffSelect?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer; height:{8}px;width:{9}px; {4}\" {5}>{1}</textarea>",
               name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId, heigh, width));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Staff/StaffSelect?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
        }

        public static MvcHtmlString ShippingInternal(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/ListingTemplate/ShippingInternal?objID={0}&num={2}&postions={6}', '500', '150');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/ListingTemplate/ShippingInternal?objID={0}&num={2}&postions={6}', '500', '150');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
        }

        public static MvcHtmlString Shipping(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/ListingTemplate/Shipping?objID={0}&num={2}&postions={6}', '570', '150');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/ListingTemplate/Shipping?objID={0}&num={2}&postions={6}', '570', '150');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
        }
        public static MvcHtmlString ShippingLocation(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/ListingTemplate/ShippingLocation?objID={0}&num={2}&postions={6}', '570', '250');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/ListingTemplate/ShippingLocation?objID={0}&num={2}&postions={6}', '570', '250');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
        }
        public static MvcHtmlString ExcludeShpLoc(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/ListingTemplate/ExcludeShpLoc?objID={0}&num={2}&postions={6}', '250', '340');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/ListingTemplate/ExcludeShpLoc?objID={0}&num={2}&postions={6}', '250', '340');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions));
        }

        #region 员工选择（包含子部门）

        /// <summary>
        /// 选择员工Html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="userNum"></param>
        /// <param name="isReadonly"></param>
        /// <param name="style"></param>
        /// <param name="attr"></param>
        /// <param name="postions">职位级别</param>
        /// <param name="deptId">初始化部门</param>
        /// <returns></returns>
        public static MvcHtmlString StaffSelectInputIncludeChild(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null, int? deptId = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input type='text' id='{0}' name='{0}' value='{1}' onclick=\"showPage('/Staff/StaffSelectIncludeChlid?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Staff/StaffSelectIncludeChlid?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
        }

        #endregion

        /// <summary>
        /// 选择员工Html(正式+试用期)
        /// </summary>
        /// <param name="postions">职位级别</param>
        /// <param name="deptId">初始化部门</param>
        /// <returns></returns>
        public static MvcHtmlString StaffSelectNormalInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string postions = null, int? deptId = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 20)
            {
                return MvcHtmlString.Create(string.Format("<input type='text' id='{0}' name='{0}' value='{1}' onclick=\"showPage('/Staff/StaffSelectNormal?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick=\"showPage('/Staff/StaffSelectNormal?objID={0}&num={2}&postions={6}&deptId={7}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, postions, deptId));
        }


        public static MvcHtmlString LibraryManageCategoryInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string platform = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/LibraryCategory/LibraryCategorySelect?objID={0}&num={2}&platform={6}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, platform));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick='openType();' {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, platform));
        }


        public static MvcHtmlString CustomerStaffStructureInput(this HtmlHelper html, string name, string value = "", int? userNum = null, bool isReadonly = true, string style = null, string attr = null, string platform = null)
        {
            if ((userNum ?? 0) <= 0) userNum = 0;
            if (userNum > 0 && userNum <= 5)
            {
                return MvcHtmlString.Create(string.Format("<input id='{0}' name='{0}' value='{1}' onclick=\"showPage('/CustomerStaffStructure/CustomerStaffStructureSelect?objID={0}&num={2}&platform={6}', '400', '430');\" {3} style=\"cursor:pointer;{4}\" {5} />",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, platform));
            }
            return MvcHtmlString.Create(string.Format("<textarea id='{0}' name='{0}' onclick='openType();' {3} style=\"cursor:pointer;{4}\" {5}>{1}</textarea>",
                name, value, userNum, (isReadonly ? "readonly='readonly'" : ""), style, attr, platform));
        }





    }
}
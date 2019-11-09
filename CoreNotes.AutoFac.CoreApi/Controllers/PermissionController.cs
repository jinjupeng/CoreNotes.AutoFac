using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
	[Route("[controller]/[action]")]
	public class PermissionController : ControllerBase
	{
		private readonly IPermissionService _permissionService;

		public PermissionController(IPermissionService permissionService)
		{
			_permissionService = permissionService;
		}

		/// <summary>
		/// 查询单条数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<MessageModel<Permission>> Get(int id)
		{
			var model = await _permissionService.QueryById(id).ConfigureAwait(false);
			MessageModel<Permission> message = new MessageModel<Permission>
			{
				Msg = "获取成功！",
				Success = true,
				Response = model
			};
			return message;
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<MessageModel<PageModel<Permission>>> GetList(int pageIndex, int pageSize)
		{
			var data = await _permissionService.QueryPage(a => a.IsDelete == false, pageIndex, pageSize)
				.ConfigureAwait(false);
			var message = new MessageModel<PageModel<Permission>>
			{
				Msg = "获取成功！",
				Success = data.DataCount > 0,
				Response = data
			};
			return message;
		}

		/// <summary>
		/// 新增权限信息
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<MessageModel<string>> Post(Permission permission)
		{
			var data = new MessageModel<string>();

			if (permission != null)
			{
				permission.IsDelete = false;
				permission.CreateTime = DateTime.Now;
				permission.ModifyTime = DateTime.Now;
				var result = await _permissionService.Add(permission).ConfigureAwait(false);
				data.Success = result > 0;
				if (data.Success)
				{
					data.Response = result.ObjToString();
					data.Msg = "添加成功";
				}
			}

			// TODO：添加日志到数据库
			return data;
		}

		/// <summary>
		/// 更新权限信息
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<MessageModel<string>> Put(Permission permission)
		{
			var data = new MessageModel<string>();
			if (permission != null && permission.Id > 0)
			{
				permission.ModifyTime = DateTime.Now;
				var result = await _permissionService.Update(permission).ConfigureAwait(false);
				data.Success = result;
				if (data.Success)
				{
					data.Response = permission?.Id.ObjToString();
					data.Msg = "添加成功";
				}
			}

			// TODO：添加日志到数据库
			return data;
		}

		/// <summary>
		/// 删除单条数据
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<MessageModel<string>> Delete(int id)
		{
			var data = new MessageModel<string>();
			if (id > 0)
			{
				var permissionDetail = await _permissionService.QueryById(id).ConfigureAwait(false);
				permissionDetail.IsDelete = true;
				data.Success = await _permissionService.Update(permissionDetail).ConfigureAwait(false);
				if (data.Success)
				{
					data.Msg = "删除成功";
					data.Response = permissionDetail?.Id.ObjToString();
				}
			}

			return data;
		}

		// TODO：获取路由树

		// TODO：获取菜单树

		// TODO：菜单权限分配
	}
}
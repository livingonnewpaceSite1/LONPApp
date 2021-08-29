﻿using FirstBlazorApp.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Mvc;
using MatBlazor;

namespace FirstBlazorApp.Pages
{
    public partial class Surveypageone:ComponentBase
    {
		
		survey_profile recordSurveyProfile = new survey_profile();
		List<province> provinces1 = new List<province>();
		List<district> districts = new List<district>();
		List<district> districts2 = new List<district>();
		List<tambon> tambons = new List<tambon>();
		static SemaphoreSlim semaphonSlim=new SemaphoreSlim(1);
		string provinceId;
		string districtId;
		string districtId2;
		string tambonId2;
		string complete = "";
		private string threatId3 = null;
		public string tambonId
		{
			get { return threatId3; }
			set
			{
				threatId3 = value;
			}
		}
		private string threatId = null;
		private string threatId4 = null;
		private string threatId2 = null;
		private string districtSel = null;
		private IJSObjectReference _jsModule;
		[Parameter]
		public string HC { get; set; }
		//EmployeeContext DBContext;
		public static class configSurvey
		{
            public static string survey_year = "2563";
            public static string survey_no_num = "1";
            public static string survey_no (string HC,int? index)
            {//-------no-------------survey year

				
				var index4 = index.ToString().PadLeft(4, '0');
				return configSurvey.survey_no_num + "|" + configSurvey.survey_year + "|" + HC + "|" + index4;

			}
			public static string HC_random(string hc)
            {
				//var index4 = (1).ToString().PadLeft(4, '0');
				string hc2 = hc.ToString() + "|" + configSurvey.randomNum();
				return configSurvey.survey_no_num + "|" + configSurvey.survey_year + "|" + hc2;
				//return 1 + "|" + configSurvey.survey_year + "|" + hc + "|" + configSurvey.randomNum;

			}public static string HC_noId(string hc)
            {
				//var index4 = (1).ToString().PadLeft(4, '0');
				string hc2 = hc.ToString() ;
				return configSurvey.survey_no_num + "|" + configSurvey.survey_year + "|" + hc2;
				//return 1 + "|" + configSurvey.survey_year + "|" + hc + "|" + configSurvey.randomNum;

			}
			public static string randomNum()
            {
				Random r = new Random();
				int num = r.Next();
				return num.ToString();
            }
			public static int timestam()
            {
				return   (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			}
			public static void commentUplog()
            {
				//await DBContext.AddItems<log_file>("log_file", new List<log_file> {
				//	new log_file {
				//		id=Convert.ToInt32(configSurvey.randomNum()),
				//		username= await JSRuntime.InvokeAsync<string>("localStorage.getItem", "name"),
				//		time1=configSurvey.timestam(),
				//		detail=detail
				//		} });
				
			}


        }
		public string SelectProvinceId
		{
			get { return threatId4; }
			set
			{
				if (value.Substring(0, 1 )== "s")
				{
					threatId4 = value.Substring(3);
				}else
				{

					threatId4 = value;

				SelectThreatValueChanged(value);
				}
			}
		}
		public string SelectDistrictId
		{
			get { return threatId2; }
			set
            {
				if (value != null && value != "")
				{


					if (value.Substring(0, 1) == "s")
					{
						threatId2 = value.Substring(3);
					}
					else if (value == "x")
					{
						threatId2 = "";
					}
					else
					{

						threatId2 = value;
						SelectDistrictValueChanged(value);
					}
				}
			

			}
		}
		public string SelectTambonId
		{
			get { return threatId3; }
			set
			{

				threatId3 = value;


			}
		}


		private async void SelectDistrictValueChanged(string id)
{//https://stackoverflow.com/questions/64802201/blazor-matblazor-how-to-catch-the-value-change-of-matselect-component
			int proId = Convert.ToInt32(id);
			if (id != null)
				tambons = await DBContext.GetByIndex<string, tambon>("tambon", id, null, "district_id", false);
            tambonId = "";
            StateHasChanged();


		}
		private async void SelectThreatValueChanged(string id)
{//https://stackoverflow.com/questions/64802201/blazor-matblazor-how-to-catch-the-value-change-of-matselect-component
			int proId = Convert.ToInt32(id);
            tambonId = "";
            districts = await DBContext.GetByIndex<int?, district>("district", proId, 0, "province_id", false);
            SelectDistrictId = "x";

            tambons = new List<tambon>();
            StateHasChanged();
			//
		}



		public void autoTab(KeyboardEventArgs e)
		{
			//var key=e.Code.Remove(e.Code.Length - 1);
			//if (key == "Digit" ||key == "Numpad")
			//{
			//	// ...
			//	recordSurveyProfile.HC = recordSurveyProfile.HC + e.Key;
			//}
   //         else
   //         {
			//	recordSurveyProfile.HC = recordSurveyProfile.HC.Remove(recordSurveyProfile.HC.Length - 1) + "|";

			//}
			
		}


		protected override async Task OnInitializedAsync()
		{
			await DBContext.OpenIndexedDb();
			provinces1 = await DBContext.GetAll<province>("province");

			await JSRuntime.InvokeVoidAsync("localStorage.setItem", "HC", HC);
			_jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./script/callFunction.js");
			if(HC!= null)
            {

		List<survey_profile>getAllSurPro= await DBContext.GetByIndex<string, survey_profile>("survey_profile", HC, null, "hc", false);
			recordSurveyProfile = getAllSurPro.First();
		//	semaphonSlim.Wait();
			//SelectThreatValueChanged(recordSurveyProfile.JUN);
			//SelectDistrictValueChanged(recordSurveyProfile.AMP);
			SelectProvinceId	="sel"+ recordSurveyProfile.JUN;
			SelectDistrictId	="sel"+	recordSurveyProfile.AMP;
			SelectTambonId		=recordSurveyProfile.TMP;
            }
            if (recordSurveyProfile.AMP != null &&recordSurveyProfile.AMP!="")
            {
                int amp = Convert.ToInt32(recordSurveyProfile.JUN);
                districts = await DBContext.GetByIndex<int?, district>("district", amp, 0, "province_id", false);

                tambons = await DBContext.GetByIndex<string, tambon>("tambon", recordSurveyProfile.AMP, null, "district_id", false);
                StateHasChanged();

			}

			//provinces1 = await DBContext.GetAll<province>("province");
		}

		protected async Task HandleValidSubmit(EditContext context)
		{
			var User = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "name");
			await DBContext.OpenIndexedDb();
			Random r = new Random();
			int num = r.Next();
			
			int updateStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			recordSurveyProfile.JUN = SelectProvinceId;
			recordSurveyProfile.AMP = SelectDistrictId;
			recordSurveyProfile.TMP = tambonId;
			var HCFromInput = configSurvey.HC_random(recordSurveyProfile.HC);

			recordSurveyProfile.survey_no = HCFromInput;
			List<survey_profile> GetHC = await DBContext.GetByIndex<string, survey_profile>("survey_profile", recordSurveyProfile.HC, null, "hc", false);
			if (GetHC.Count != 0)
			{
				recordSurveyProfile.last_edit = updateStamp;
				_ = await DBContext.UpdateItems<survey_profile>("survey_profile", new List<survey_profile> { recordSurveyProfile });
				_ = await DBContext.AddItems<log_file>("log_file", new List<log_file> { new log_file { id = num, username = User, time1 = updateStamp, detail = "แก้ไข" + recordSurveyProfile.HC + "profile" } });
			}
			else
			{
				recordSurveyProfile.id = num;
				await DBContext.AddItems<survey_profile>("survey_profile", new List<survey_profile>() { recordSurveyProfile });

				//update survey_profile set create_survey='".date("U")."' where HC='$HC'
				//districts =await DBContext.GetByIndex<int?,district>("district", proId,0,"province_id",false);
				var getAll = await DBContext.GetAll<survey_profile>("survey_profile");
				List<survey_profile> updateSurPro = await DBContext.GetByIndex<string, survey_profile>("survey_profile", recordSurveyProfile.HC, null, "hc", false);
				updateSurPro.First().create_survey = updateStamp;
				await DBContext.UpdateItems<survey_profile>("survey_profile", new List<survey_profile> { updateSurPro.First() });
				//$log="insert into log_file ( id,username,time1,detail) values('','$username','".date("U") ."','เพิ่ม $HC')";
				await DBContext.AddItems<log_file>("log_file", new List<log_file>{
		new log_file{
			id=num,
				username=User,
				time1=(int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
				detail="เพิ่ม "+recordSurveyProfile.HC
		}
		});

			}

			List<survey_staff> getSurveyStaff = await DBContext.GetAll<survey_staff>("survey_staff");
			var getStaffBySurveyYear=getSurveyStaff.Where(x => x.HC.Contains(recordSurveyProfile.HC)).ToList();
			//$query1="insert into survey_staff (HC,survey_year,survey_no,staff) value('$HC','$survey_year','$survey_no','$staff')";
			if (getStaffBySurveyYear.Count == 0)
			{

				_ = await DBContext.AddItems<survey_staff>("survey_staff", new List<survey_staff> {

					new survey_staff
					{
						//HC =  recordSurveyProfile.HC,
						HC =recordSurveyProfile.HC,
						survey_year = configSurvey.survey_year,
						survey_no = configSurvey.survey_no_num,
						staff=User,
						ch1_st=(int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds

					}

				});
			}
			
			
			HC=recordSurveyProfile.HC;
			//$log="insert into log_file ( id,username,time1,detail) values('','$username','".date("U") ."','เพิ่ม $HC')";
			complete = "เรียบร้อย";
		}
		private async Task gotoPage(string HC_nextPage)
        {
			NavigationManager.NavigateTo("/surveya1/"+ HC_nextPage);

		}
		public async Task gotoSelAsync()
		{
			//{
			//	await DBContext.OpenIndexedDb();
			//	districts = await DBContext.GetByIndex<int?, district>("district", Convert.ToInt32(recordSurveyProfile.JUN), 0, "province_id", false);
			//	tambons = await DBContext.GetByIndex<string, tambon>("tambon", "9402", null, "district_id", false);

			SelectProvinceId = "94";
			//	//SelectDistrictId = recordSurveyProfile.AMP;
			recordSurveyProfile.TMP = "940109";
			recordSurveyProfile.AGRI_NO = "sdsdsdsdsdsdsds";
			//	recordSurveyProfile.AMP = "9603";
		}

		public MatTheme theme = new MatTheme { Primary = "#AAAA00", Secondary = "#00AAAA", OnSecondary = "#FAFAAA" };
		void Navigate(string HC_nextPage)
		{
			UriHelper.NavigateTo("/surveya1/" + HC_nextPage);
		}

	}
}

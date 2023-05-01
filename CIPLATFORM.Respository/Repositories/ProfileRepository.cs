using CIPLATFORM.Entities.Data;
using CIPLATFORM.Entities.Models;
using CIPLATFORM.Entities.ViewModels;
using CIPLATFORM.Respository.Interface;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace CIPLATFORM.Respository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly CiPlatformContext _CiPlatformContext;

        public ProfileRepository(CiPlatformContext CiPlatformContext)
        {
            _CiPlatformContext = CiPlatformContext;
        }


        public ProfileViewModel getUser(int UId)
        {
            User user = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            List<UserSkill> us = _CiPlatformContext.UserSkills.Include(m => m.Skill).Where(m => m.UserId == UId).ToList();
            List<Skill> skills = _CiPlatformContext.Skills.Include(m => m.UserSkills).ToList();

            foreach (var userSkill in us)
            {
                skills = skills.Where(skill => skill.SkillId != userSkill.SkillId).ToList();
            }

            ProfileViewModel pm = new ProfileViewModel();
            if (user != null)
            {
                {
                    pm.UserId = user.UserId;
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;
                    pm.Avatar = user.Avatar;
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                    pm.userSkills = us;
                    pm.skills = skills;
                }
            }
            return pm;
        }
        public bool changepassword(ProfileViewModel user, int UId)
        {
            User pm = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);
            if (pm != null)
            {
                bool isPasswordMatch = Crypto.VerifyHashedPassword(pm.Password, user.resetPass.OldPassword);
                if (isPasswordMatch)
                {
                    {
                        pm.Password = Crypto.HashPassword(user.resetPass.Password);

                    }
                    _CiPlatformContext.Users.Update(pm);
                    _CiPlatformContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool saveProfile(ProfileViewModel user, int UId)
        {

            User pm = _CiPlatformContext.Users.FirstOrDefault(x => x.UserId == UId);



            if (pm != null)
            {
                {
                    pm.FirstName = user.FirstName;
                    pm.LastName = user.LastName;

                    if (user.Avatarfile != null)
                    {
                        pm.Avatar = user.Avatarfile.FileName;
                    }
                    pm.EmployeeId = user.EmployeeId;
                    pm.Title = user.Title;
                    pm.Department = user.Department;
                    pm.ProfileText = user.ProfileText;
                    pm.Department = user.Department;
                    pm.WhyIVolunteer = user.WhyIVolunteer;
                    pm.CountryId = user.CountryId;
                    pm.CityId = user.CityId;
                    pm.LinkedInUrl = user.LinkedInUrl;
                }
                _CiPlatformContext.Users.Update(pm);
                _CiPlatformContext.SaveChanges();



                List<UserSkill> us = _CiPlatformContext.UserSkills.Where(x => x.UserId == UId).ToList();
                _CiPlatformContext.UserSkills.RemoveRange(us);

                foreach (var skill in user.skillsToAdd)
                {
                    UserSkill addSkill = new UserSkill();
                    {
                        addSkill.UserId = UId;
                        addSkill.SkillId = skill;
                    }
                    _CiPlatformContext.UserSkills.Add(addSkill);
                    _CiPlatformContext.SaveChanges();
                }
                return true;
            }
            return true;
        }

        public bool ContactUs(ProfileViewModel obj)
        {
            ContactU cu = new ContactU();
            {
                cu.Name = obj.contactus.Name;
                cu.Email = obj.contactus.Email;
                cu.Subject = obj.contactus.subject;
                cu.Message = obj.contactus.Message;
            }

            _CiPlatformContext.ContactUs.Add(cu);
            _CiPlatformContext.SaveChanges();


            #region Send Mail
            var mailBody = "<h2>I hope this email finds you well. My name is " + obj.contactus.Name + " and I wanted to take a moment to you.</h1>" + "<h1>" + obj.contactus.Message + "</h1><br><h2>" + "</h2>";

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(obj.contactus.Email));
            email.To.Add(MailboxAddress.Parse("jainamshah492@gmail.com"));
            email.Subject = obj.contactus.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jainamshah492@gmail.com", "aflpkkevlfxzmmtx");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail

            return true;
        }

        public List<MissionApplication> TimeMission(int UId)
        {
            List<MissionApplication> missions = _CiPlatformContext.MissionApplications.Include(m => m.Mission).Where(x => x.UserId == UId && x.ApprovalStatus == "Approve" && x.Mission.MissionType == "Time").ToList();
            return missions;
        }
        public List<MissionApplication> GoalMission(int UId)
        {
            List<MissionApplication> missions = _CiPlatformContext.MissionApplications.Include(m => m.Mission).Where(x => x.UserId == UId && x.ApprovalStatus == "Approve" && x.Mission.MissionType == "Goal").ToList();
            return missions;
        }
        public List<Timesheet> timesheet(int UId)
        {
            List<Timesheet> tts = _CiPlatformContext.Timesheets.Include(m => m.Mission).Where(x => x.UserId == UId && x.Mission.MissionType == "Time" && x.DeletedAt == null).ToList();
            return tts;
        }
        public List<Timesheet> goaltimesheet(int UId)
        {
            List<Timesheet> gts = _CiPlatformContext.Timesheets.Include(m => m.Mission).Where(x => x.UserId == UId && x.Mission.MissionType == "Goal" && x.DeletedAt == null).ToList();
            return gts;
        }
        public ProfileViewModel GetTimsheet(int UId)
        {
            List<MissionApplication> tm = TimeMission(UId);
            List<MissionApplication> gm = GoalMission(UId);
            List<Timesheet> ts = timesheet(UId);
            List<Timesheet> gs = goaltimesheet(UId);
            ProfileViewModel Timemodel = new ProfileViewModel();
            {
                Timemodel.timemissions = tm;
                Timemodel.goalmissions = gm;
                Timemodel.timesheets = ts;
                Timemodel.goaltimesheets = gs;
            }
            return Timemodel;

        }

        public ProfileViewModel GetActivity(int obj, int UId)
        {
            Timesheet timesheet = _CiPlatformContext.Timesheets.FirstOrDefault(x => x.TimesheetId == obj);
            ProfileViewModel pm = new ProfileViewModel();
            {
                pm.Timesheet.Time = timesheet.Time;
                pm.Timesheet.DateVolunteereed = timesheet.DateVolunteereed;
                pm.Timesheet.Notes = timesheet.Notes;
                pm.Timesheet.MissionId = timesheet.MissionId;
                pm.Timesheet.TimesheetId = obj;
                pm.Timesheet.Action = timesheet.Action;
                pm.Hours = timesheet.Time.Value.Hours;
                pm.Minutes = timesheet.Time.Value.Minutes;
                pm.timemissions = TimeMission(UId);
                pm.goalmissions = GoalMission(UId);
            }
            return pm;
        }
        public bool updatetimesheet(ProfileViewModel obj, int tid, int UId)
        {
            Timesheet ts = _CiPlatformContext.Timesheets.FirstOrDefault(x => x.TimesheetId == tid);
            if (ts != null)
            {
                ts.MissionId = obj.Timesheet.MissionId;
                if (obj.Hours != null || obj.Minutes != null)
                {
                    ts.Time = new TimeSpan(obj.Hours, obj.Minutes, 0);
                }
                else
                {
                    ts.Time = new TimeSpan(0, 0, 0);
                }
                if (obj.Timesheet.Action != null)
                {
                    ts.Action = obj.Timesheet.Action;
                }
                else
                {
                    ts.Action = 0;
                }
                ts.DateVolunteereed = obj.Timesheet.DateVolunteereed;
                ts.Notes = obj.Timesheet.Notes;
                ts.UpdatedAt = DateTime.Now;

                _CiPlatformContext.Timesheets.Update(ts);
                _CiPlatformContext.SaveChanges();

                return false;
            }
            else
            {
                Timesheet Ts = new Timesheet();
                {
                    Ts.MissionId = obj.Timesheet.MissionId;
                    Ts.UserId = UId;
                    Ts.Time = new TimeSpan(obj.Hours, obj.Minutes, 0);
                    if (obj.Timesheet.Action != null)
                    {
                        Ts.Action = obj.Timesheet.Action;
                    }
                    else
                    {
                        Ts.Action = 0;
                    }
                    //Ts.Time = new TimeSpan(obj.Hours, obj.Minutes, 0);
                    Ts.DateVolunteereed = obj.Timesheet.DateVolunteereed;
                    Ts.Notes = obj.Timesheet.Notes;
                    _CiPlatformContext.Timesheets.Add(Ts);
                    _CiPlatformContext.SaveChanges();
                }
                return true;
            }
        }

        public bool deletetimesheet(int tid)
        {
            if (tid != 0)
            {
                Timesheet timesheet = _CiPlatformContext.Timesheets.FirstOrDefault(x => x.TimesheetId == tid);
                timesheet.DeletedAt = DateTime.Now;
                _CiPlatformContext.Timesheets.Update(timesheet);
                _CiPlatformContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

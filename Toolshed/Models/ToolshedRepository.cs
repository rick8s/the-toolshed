﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Toolshed.Models
{
    public class ToolshedRepository
    {
        private ToolshedContext _context;
        public ToolshedContext Context { get { return _context; } }

        public ToolshedRepository()
        {
            _context = new ToolshedContext();
        }
        public ToolshedRepository(ToolshedContext a_context)
        {
            _context = a_context;
        }
        public List<ToolshedUser> GetAllUsers()
        {
            var query = from users in _context.ToolshedUsers select users;
            return query.ToList();
        }

        public ToolshedUser GetUserByUserName(string username)
        {
           
            var query = from user in _context.ToolshedUsers where user.UserName == username select user;

            return query.SingleOrDefault();
        }

        public bool IsUserNameAvailable(string username)
        {
            bool available = false;
            try
            {
                ToolshedUser some_user = GetUserByUserName(username);
                if (some_user == null)
                {
                    available = true;
                }
            }
            catch (InvalidOperationException) { }

            return available;
        }

        public List<ToolshedUser> SearchByUserName(string username)
        {
            var query = from user in _context.ToolshedUsers select user;
            List<ToolshedUser> found_users = query.Where(user => user.UserName.Contains(username)).ToList();
            found_users.Sort();
            return found_users;
        }

        public List<ToolshedUser> SearchByName(string search_term)
        {
            var query = from user in _context.ToolshedUsers select user;
            List<ToolshedUser> found_users = query.Where(user => Regex.IsMatch(user.FirstName, search_term, RegexOptions.IgnoreCase) || Regex.IsMatch(user.LastName, search_term, RegexOptions.IgnoreCase)).ToList();
            found_users.Sort();
            return found_users;
        }

        public List<Tool> GetAllTools()
        {
            // SQL: select * from Tools;
            var query = from tool in _context.Tools select tool;
            List<Tool> found_tools = query.ToList();
            found_tools.Sort();
            return found_tools;
        }

        public bool CreateTool(ToolshedUser toolshed_user1, string name, string category, string descrip, string pic, int toolid)
        {
            Tool a_tool = new Tool { Name = name, Owner = toolshed_user1, Category = category, Description = descrip, Image = pic, ToolId = toolid };
            bool is_added = true;
            try
            {
                Tool added_tool = _context.Tools.Add(a_tool);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }

        public List<Tool> GetAvailableTools()
        {
            var query = from tool in _context.Tools select tool;
            List<Tool> found_available = query.Where(tool => tool.Available == true).ToList();

            List<Tool> found_available_sorted = found_available.OrderBy(tool => tool.Available).ToList();
            return found_available_sorted;
        }

        public List<Tool> GetUserTools(ToolshedUser user)
        {
                      
            if (user != null)
            {
                var query = from u in _context.ToolshedUsers where u.UserId == u.UserId select u;
                ToolshedUser found_user = query.Single<ToolshedUser>();
                if (found_user == null)
                {
                    return new List<Tool>();
                }
                return found_user.Tools;
            }
            else
            {
                return new List<Tool>();
            }
        }

        public void DeleteAllUsers()
        {
            Context.ToolshedUsers.RemoveRange(Context.ToolshedUsers);
            Context.SaveChanges();
        }

        public bool CreateToolshedUser(ApplicationUser app_user, string new_user_name)
        {
            bool handle_is_available = this.IsUserNameAvailable(new_user_name);
            if (handle_is_available)
            {
                ToolshedUser new_user = new ToolshedUser { RealUser = app_user, UserName = new_user_name };
                bool is_added = true;
                try
                {
                    ToolshedUser added_user = _context.ToolshedUsers.Add(new_user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    is_added = false;
                }
                return is_added;
            }
            else
            {
                return false;
            }
        }
    }
}
using System;

namespace TodoApp.Models.ViewModels;

public class EditUserViewModel
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmPassword { get; set; }
}

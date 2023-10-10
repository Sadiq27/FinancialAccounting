using ProjectORM.Commands.Base;
using ProjectORM.Mediator.Base;
using ProjectORM.Messages;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using ProjectORM.ViewModels.Base;
using System;
using System.Windows;

namespace ProjectORM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMessenger messenger;


        private string? username;
        private string? password;

        public string? Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string? Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private CommandBase? loginCommand;
        public CommandBase LoginCommand => this.loginCommand ??= new CommandBase(
            execute: () =>
            {
                try
                {
                    User user = userRepository.GetUserByUsernameAndPassword(Username, Password);

                    if (user != null)
                    {
                        this.messenger.Send(new NavigationMessage(App.Container.GetInstance<HomeViewModel>()));
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            },
            canExecute: () => true
        );


        public CommandBase? registerCommand;
        public CommandBase RegisterCommand => this.registerCommand ??= new CommandBase(
                execute: () =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(Username))
                        {
                            MessageBox.Show("Username is required.");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            MessageBox.Show("Password is required.");
                            return;
                        }

                        if (Password.Length < 8)
                        {
                            MessageBox.Show("Password must be at least 8 characters long.");
                            return;
                        }

                        this.userRepository.AddUser(new User()
                        {
                            Username = this.Username,
                            Password = this.Password
                        });
                        MessageBox.Show("Successful registration");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }

                    this.Username = string.Empty;
                    this.Password = null;
                },
                canExecute: () => true
            );
        
        public LoginViewModel(IUserRepository userRepository, IMessenger messenger)
        {
            this.userRepository = userRepository;
            this.messenger = messenger;
        }
    }
}


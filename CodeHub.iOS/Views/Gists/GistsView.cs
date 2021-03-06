using System;
using CodeFramework.ViewControllers;
using CodeHub.Core.ViewModels.Gists;
using MonoTouch.Dialog;

namespace CodeHub.iOS.Views.Gists
{
    public abstract class GistsView : ViewModelCollectionDrivenViewController
    {
        public override void ViewDidLoad()
        {
            NoItemsText = "No Gists".t();

            base.ViewDidLoad();

            var vm = (GistsViewModel) ViewModel;
            BindCollection(vm.Gists, x =>
            {
                var str = string.IsNullOrEmpty(x.Description) ? "Gist " + x.Id : x.Description;
                var sse = new NameTimeStringElement
                {
                    Time = x.UpdatedAt.ToDaysAgo(),
                    String = str,
                    Lines = 4,
                    Image = Theme.CurrentTheme.AnonymousUserImage
                };

                sse.Name = (x.User == null) ? "Anonymous" : x.User.Login;
                sse.ImageUri = (x.User == null) ? null : new Uri(x.User.AvatarUrl);
                sse.Tapped += () => vm.GoToGistCommand.Execute(x);
                return sse;
            });
        }
    }
}


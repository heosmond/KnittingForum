using KnittingForum.Data;

namespace KnittingForum.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; private set; }
        public List<Discussion> Discussions { get; private set; }

        public ProfileViewModel(ApplicationUser user, List<Discussion> discussions)
        {
            User = user;
            Discussions = discussions;
        }
    }
}

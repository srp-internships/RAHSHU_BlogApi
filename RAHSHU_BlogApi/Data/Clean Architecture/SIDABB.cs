namespace RAHSHU_BlogApi.Data.NewFolder
{
    class UserRepository
    {
        // Get 
        // Create 
        // Update 
        // Delete 
    }

    class PostRepository
    {
        // Get 
        // Create 
        // Update 
        // Delete 
    }

    class UserService
    {
        private readonly UserRepository userRepository;
        public UserService(UserRepository repository)
        {
            userRepository = repository;
        }


    }

    class PostService
    {
        private readonly PostRepository userRepository;
        public PostService(PostRepository repository)
        {
            userRepository = repository;
        }

    }

    class JSonPlaceholderService
    {
        //FetchUsers() -> List<User> 
        //FetchPosts() -> List<Post> 
    }

    class SeedService
    {
        JSonPlaceholderService jSonPlaceholderService;
        UserRepository userRepository;
        void Seed()
        {
            // jSonPlaceholderService.FetchUsers(); 
            // jSonPlaceholderService.FetchUsers(); 

            // logic ... 

        //    userRepository.Create(users);
        }
    }


    class SeedController
    {

    }
}

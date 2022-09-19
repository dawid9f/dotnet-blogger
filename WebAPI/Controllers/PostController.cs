using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary = "Wszystkie posty")]
        [Route("getposts")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.GetAllPosts());
        }

        [SwaggerOperation(Summary = "Post o wskazanym id")]
        [Route("getpost/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var post = _postService.GetPostById(id);
            if (post != null)
            {
                return Ok(post);
            }
            else
            {
                return NotFound("Brak Postu o takim ID");
            }
        }

        [SwaggerOperation(Summary = "Dodawanie posta")]
        [Route("add")]
        [HttpPost]
        public IActionResult Create(CreatePostDto newPost)
        {
            return Created("Nowy",_postService.AddNewPost(newPost));
        }

        [SwaggerOperation(Summary = "Edycja posta")]
        [Route("update")]
        [HttpPut]
        public IActionResult Update(UpdatePostDto updatePost)
        {
            _postService.UpdatePost(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Usuwanie posta")]
        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return NoContent();
        }
    }
}

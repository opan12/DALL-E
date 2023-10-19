using dall_e.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Diagnostics;
using OpenAI.Interfaces;

namespace dall_e.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpenAIService _openAiService;


        public HomeController(ILogger<HomeController> logger, IOpenAIService openAiService)
        {
            _logger = logger;
            _openAiService = openAiService;

        }

        [HttpGet]

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();

            return View(viewModel);
        }
        [HttpPost]

        public async Task<IActionResult> Index(HomeIndexViewModel viewModel)
        { /*
            var imageResult = await _openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = viewModel.Prompt,
                N = viewModel.ImageCount,
                Size = StaticValues.ImageStatics.Size.Size512,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "sude"
            });*/

            var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
    {

        ChatMessage.FromUser(viewModel.Prompt),

    },
                Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo,
                MaxTokens = 4000//optional
            });
            if (completionResult.Successful)
            {
                viewModel.ChatGPTResponse = completionResult.Choices.First().Message.Content;
            }

            return View(viewModel);
            [HttpGet]
            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    } }
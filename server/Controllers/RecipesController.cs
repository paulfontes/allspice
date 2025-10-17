namespace allspice.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController(Auth0Provider auth, RecipesService recipesService) : ControllerBase
{
    private readonly Auth0Provider _auth = auth;
    private readonly RecipesService _recipesService = recipesService;


    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = user.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public ActionResult<Recipe> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetAllRecipes();
            return Ok(recipes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}











namespace allspice.Services;


public class RecipesService(RecipeRepository repo)

{
    private readonly RecipeRepository _repo = repo;

    public Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    public List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _repo.GetAllRecipes();
        return recipes;
    }
}

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

    public Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repo.GetRecipeById(recipeId);
        return recipe;
    }

    public Recipe UpdateRecipe(int recipeId, Recipe recipeData, Account userInfo)
    {
        Recipe recipe = GetRecipeById(recipeId);

        if (recipe.CreatorId != userInfo.Id)
        {
            throw new Exception($"You cannot alter someones data!");
        }

        recipe.Img = recipeData.Img ?? recipe.Img;
        recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
        recipe.Title = recipeData.Title ?? recipe.Title;
        recipe.Category = recipeData.Category ?? recipe.Category;

        _repo.UpdateRecipe(recipe);

        return recipe;

    }
}
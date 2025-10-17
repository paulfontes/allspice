
namespace allspice.Repositories;



public class RecipeRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;

    public Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, instructions, img, category, creator_id)

        VALUES
        (@Title, @Instructions, @Img, @Category, @CreatorId);

        SELECT
         recipes.*,
         accounts.*
         FROM recipes
         JOIN accounts ON accounts.id = recipes.creator_id
         WHERE recipes.id = LAST_INSERT_ID()
        ;";

        Recipe recipe = _db.Query(sql,
        (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }
        , recipeData).SingleOrDefault();
        return recipe;
    }

    public List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT
            recipes.*,
            accounts.*
        FROM 
            recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        ;";

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator).ToList();
        return recipes;
    }

    public Recipe GetRecipeById(Recipe recipe)
    {
        string sql = @"
        
        ;";
    }




    private Recipe PopulateCreator(Recipe recipe, Account creator)
    {
        recipe.Creator = creator;
        return recipe;
    }


}
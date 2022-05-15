#pragma once
#include <MC/Recipes.hpp>
#include <MC/Recipe.hpp>
#include <MC/Actor.hpp>
#include <MC/Level.hpp>
#include <MC/HashedString.hpp>
#include <MC/ItemInstance.hpp>
#include <MC/ItemStackBase.hpp>
#include <MC/Item.hpp>

#include <MC/SetTitlePacket.hpp>
#include <MC/TitleRawCommand.hpp>
#include <MC/Command.hpp>
#include <MC/Level.hpp>	

#include <MC/CommandSelector.hpp>

#include <LoggerAPI.h>
using System::String;

#include "../clix.hpp"
using namespace clix;

#pragma unmanaged
void _Test(Level& level) {
	Logger logger("RecipeHelper");
	auto& recipes = level.getRecipes();
	auto& recipesAllTags = recipes.getRecipesAllTags();



	for (auto& kv : recipesAllTags)
	{
		logger.info("HashedString:<{},{}>", kv.first.getString(), kv.first.getHash());
		for (auto& _kv : kv.second)
		{
			auto& pRecipe = _kv.second;
			auto& ingredients = pRecipe->getIngredients();
			string str;
			for (auto& item : ingredients) {
				str += " , ";
				str += item.descriptor.getSerializedNameAndAux();

			}

			logger.info("string:<{}>,Ingredients:<{}>,IngredientsCount:<{}>", _kv.first, str, ingredients.size());
		}
	}
}

#pragma managed
namespace AutomaticCraft::RecipeHelper {
	public ref class Recipes {
	public:
		static void Test(System::IntPtr l) {
			_Test(*(Level*)(void*)l);
		}
	};
}



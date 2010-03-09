using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CodeCampServer.UI.Services;
using MvcContrib.UI.InputBuilder;
using MvcContrib.UI.InputBuilder.Attributes;
using MvcContrib.UI.InputBuilder.Conventions;
using MvcContrib.UI.InputBuilder.InputSpecification;
using MvcContrib.UI.InputBuilder.Views;

namespace CodeCampServer.UI.Helpers.Extensions
{
	public static class MvcHelperExtensions
	{
		public static IInputSpecification<TypeViewModel> InputFormForModel<T>(this HtmlHelper<T> _htmlHelper) where T : class
		{
			return new InputTypeSpecification<T>
			       	{
			       		Model = new ViewModelFactory<T>(_htmlHelper, InputBuilder.Conventions.ToArray(), new DefaultNameConvention(), InputBuilder.TypeConventions.ToArray()).Create(_htmlHelper.ViewData.Model.GetType()),
			       		HtmlHelper = _htmlHelper,
			       	};

		}
        
		public static string GetControllerName(this Type controllerType)
		{
			return controllerType.Name.Replace("Controller", string.Empty);
		}

		public static string GetActionName(this LambdaExpression actionExpression)
		{
			return ((MethodCallExpression)actionExpression.Body).Method.Name;
		}


		public static PropertyInfo[] VisibleProperties(this HtmlHelper helper, IEnumerable Model)
		{
			return Model.GetType().GetElementType().GetProperties().Where(info => info.PropertyType != typeof(Guid)).ToArray();
		}
		public static PropertyInfo[] VisibleProperties(this Object model)
		{
			return model.GetType().GetProperties().Where(info => info.PropertyType != typeof(Guid)).ToArray();
		}

		public static string GetLabelForModel(this HtmlHelper<IEnumerable> helper)
		{
			return helper.ViewData.Model.GetType().GetElementType().GetAttribute<LabelAttribute>().Label;
		}

		public static string GetLabelForModel(this HtmlHelper<Object> helper)
		{
			return helper.ViewData.Model.GetType().GetAttribute<LabelAttribute>().Label;
		}

		public static string GetLabel(this PropertyInfo propertyInfo)
		{
			var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
			return meta.GetDisplayName();
			//if (propertyInfo.HasCustomAttribute<LabelAttribute>())
			//{
			//    return propertyInfo.GetAttribute<LabelAttribute>().Value;
			//}
			//return propertyInfo.Name.ToSeparatedWords();
		}
	}

	public static class ButtonExtensions
	{
		public static string ButtonAddIcon(this HtmlHelper helper)
		{
			return string.Format("<button type='submit' class=\"{0}\" ><span class=\"{1}\"></span></button>", Css.IconButton, Css.AddIcon);
		}
		public static string ButtonEditIcon(this HtmlHelper helper)
		{
			return string.Format("<button type=\"submit\" class=\"{0}\"><span class=\"{1}\"></span></button>", Css.IconButton, Css.EditIcon);
		}
		public static string SubmitWithText(this HtmlHelper helper, string text)
		{
			return string.Format("<button type=\"submit\" class=\"{0} {1} \">{2}</button>", Css.Accept, Css.PrimaryButton, text);
		}


		public static string PrimaryButton(this HtmlHelper helper)
		{
			return string.Format("<button type=\"submit\" class=\"{0}\"></button>", Css.PrimaryButton);
		}


		public static string EditWithText(this HtmlHelper helper)
		{
			return string.Format("<button type='submit' class=\"{0} {1}\"><span class=\"{2}\"></span>Edit</button>", Css.PrimaryButton, Css.WithIconLeft, Css.EditIcon);
		}

		public static string EditButton(this HtmlHelper helper, string text)
		{
			return string.Format("<button type='submit' class=\"{0} {1}\"><span class=\"{2}\"></span>{3}</button>", Css.PrimaryButton, Css.WithIconLeft, Css.EditIcon, text);
		}

		public static string LogInWithText(this HtmlHelper helper)
		{
			return string.Format("<button type=\"submit\" class=\"{0} {1}\">Log In</button>", Css.Accept, Css.PrimaryButton);
		}

		public static string NewWithText(this HtmlHelper helper)
		{
			return string.Format("<button type=\"submit\" class=\"{0} {1}\"><span class=\"{2}\"></span>New</button>", Css.PrimaryButton, Css.WithIconLeft, Css.AddIcon);
		}

		public static string EditImageButton(this HtmlHelper helper, string url)
		{
			return ImageButton(helper, url, "Edit", "/images/icons/application_edit.png");
		}

		public static string AddImageButton(this HtmlHelper helper, string url)
		{
			return ImageButton(helper, url, "Add", "/images/icons/application_add.png");
		}

		public static string ImageButton(this HtmlHelper helper, string url, string altText, string imageFile)
		{
			if (!HttpContext.Current.User.Identity.IsAuthenticated)
				return string.Empty;

			IReturnUrlManager manager = ReturnUrlManagerFactory.GetDefault();

			var targetUrl = manager.GetTargetUrlWithReturnUrl(url);
			return string.Format("<a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" /></a>", targetUrl, imageFile, altText);
		}	
	}
}
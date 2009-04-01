using CodeCampServer.UI.Helpers.ViewPage;
using CodeCampServer.UI.Helpers.ViewPage.InputBuilders;
using StructureMap.Configuration.DSL;

namespace CodeCampServer.Infrastructure.UI
{
	public class UIRegistry : Registry
	{
		protected override void configure()
		{
			ForRequestedType<IInputBuilderFactory>()
				.TheDefault.Is.OfConcreteType<InputBuilderFactory>()
				.TheArrayOf<IInputBuilder>()
				.Contains(x =>
				          	{
				          		x.OfConcreteType<NoInputBuilder>();
				          		x.OfConcreteType<HiddenInputBuilder>();
				          		x.OfConcreteType<CheckboxInputBuilder>();
				          		x.OfConcreteType<YesNoRadioInputBuilder>();
				          		x.OfConcreteType<RadioInputBuilder>();
				          		x.OfConcreteType<DateInputBuilder>();
				          		x.OfConcreteType<EnumerationInputBuilder>();
				          		x.OfConcreteType<TextBoxInputBuilder>();
				          		x.OfConcreteType<TrackInputBuilder>();
				          		x.OfConcreteType<TimeSlotInputBuilder>();
				          		x.OfConcreteType<SpeakerInputBuilder>();
                                x.OfConcreteType<UserInputBuilder>();
				          	});
		}
	}
}
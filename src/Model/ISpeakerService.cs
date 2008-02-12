using System.Collections.Generic;
using CodeCampServer.Model.Domain;
using StructureMap;

namespace CodeCampServer.Model
{
    [PluginFamily(Keys.DEFAULT)]
    public interface ISpeakerService
    {
        //TODO:  The below 3 are duplicates - Palermo
        Speaker GetSpeakerByDisplayName(string displayName);
        Speaker GetSpeakerByEmail(string email);
        IEnumerable<Speaker> GetSpeakers(Conference conference, int page, int perPage);

        Speaker SaveSpeaker(string emailAddress, string firstName, string lastName, 
                            string website, string comment, string displayName, 
                            string profile, string avatarUrl);
    }
}
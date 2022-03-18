namespace Unit.Tests.Application;

using Products.Cli.Application.Utils;

public static class MockedData
{
    public static IEnumerable<object[]> InvalidData => new List<object[]>
    {
        new object[] { null,null },
        new object[] { "","" },
        new object[] { null, InvalidJson },
        new object[] { "", InvalidJson },
        new object[] { Constants.CAPTERRA_NAME, null },
        new object[] { Constants.SFTW_ADVICE_NAME, null },
        new object[] { Constants.CAPTERRA_NAME, "" },
        new object[] { Constants.SFTW_ADVICE_NAME, "" },
        new object[] { Constants.SFTW_ADVICE_NAME, InvalidJson },
        new object[] { Constants.SFTW_ADVICE_NAME, ValidJson },
        new object[] { Constants.CAPTERRA_NAME, ValidYml },
    };

    public static IEnumerable<object[]> ValidData => new List<object[]>
    {
        new object[] { Constants.CAPTERRA_NAME, ValidJson },
        new object[] { Constants.SFTW_ADVICE_NAME, ValidYml },
    };

    public const string ValidYml = @"---
    -
      tags: ""Bugs & Issue Tracking,Development Tools""
      name: ""GitGHub""
      twitter: ""github""
    -
      tags: ""Instant Messaging & Chat,Web Collaboration,Productivity""
      name: ""Slack""
      twitter: ""slackhq""
    -
      tags: ""Project Management,Project Collaboration,Development Tools""
      name: ""JIRA Software""
      twitter: ""jira""
    ";

    public const string InvalidYml = @"---
    -
      tags: ""Bugs & Issue Tracking,Development Tools""
        name: ""GitGHub""
      twitter: ""github""
    -
      tags: ""Instant Messaging & Chat,Web Collaboration,Productivity""
        name: ""Slack""
      twitter: ""slackhq""
    -
      tags: ""Project Management,Project Collaboration,Development Tools""
        name: ""JIRA Software""
      twitter: ""jira""
    ";

    public const string InvalidJson = @"
    {
        ""products"": [
                ""categories"": [
                    ""Customer Service"",
                    ""Call Center""
                ],
                ""twitter"": ""@freshdesk"",
                ""title"": ""Freshdesk""
            },
            {
                ""categories"": [
                    ""CRM"",
                    ""Sales Management""
                ],
                ""title"": ""Zoho""
            }
        ]
    }";

    public const string ValidJson = @"
    {
        ""products"": [
            {
                ""categories"": [
                    ""Customer Service"",
                    ""Call Center""
                ],
                ""twitter"": ""@freshdesk"",
                ""title"": ""Freshdesk""
            },
            {
                ""categories"": [
                    ""CRM"",
                    ""Sales Management""
                ],
                ""title"": ""Zoho""
            }
        ]
    }";
}


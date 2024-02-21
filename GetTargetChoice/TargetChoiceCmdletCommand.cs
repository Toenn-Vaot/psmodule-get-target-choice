using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace GetTargetChoice
{
    /// <summary>
    /// <para type="synopsis">Function to ask question to user to choose between Debug or Release target</para>
    /// <para type="description">This function ask question to the user and wait the choice of the user between '0' (Debug) and '1' (Release).</para>
    /// <para type="description">When the user choices a right option, the function returns the target string resulting : Debug if it is '0', Release if it is '1'</para>
    /// <example>
    /// <para>Usage</para>
    /// <para><code>Get-TargetChoice("What the target [0-Debug] / [1-Release] ?")</code></para>
    /// </example>
    /// </summary>
    /// <para type="link" uri="https://github.com/toenn-vaot/psmodule-get-target-choice">Source code</para>
    [Cmdlet(VerbsCommon.Get, "TargetChoice")]
    [OutputType(typeof(string))]
    public class TargetChoiceCmdletCommand : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The question asked user</para>
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "What is the question you want to ask ?", ValueFromPipeline = true)]
        [Alias("q")]
        public string Question { get; set; }
        
        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var targets = new Collection<string> { "Debug", "Release" };
            var debug = new ChoiceDescription("&Debug", "Continue in DEBUG mode");
            var release = new ChoiceDescription("&Release", "Continue in RELEASE mode");
            var options = new Collection<ChoiceDescription>{ debug, release };

            var choice = Host.UI.PromptForChoice("", Question, options, 0);
            WriteVerbose($"The target choice is {choice} corresponding to {targets[choice]}");

            WriteObject(targets[choice]);
        }
    }
}

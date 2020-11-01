using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace WebhookResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleUtils.SetTitle("Discord Webhook Resolver");
                ConsoleUtils.Log("Enter Webhook URL: ");
                string url = Console.ReadLine();
                HttpClient client = new HttpClient();
                var response = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                if (response == "{\"message\": \"Invalid Webhook Token\", \"code\": 50027}")
                    ConsoleUtils.LogError("Invalid Webhook URL");
                else
                {
                    string domain = "";

                    if (url.Contains("canary."))
                        domain = "canary.discord.com";
                    else if (url.Contains("ptb."))
                        domain = "ptb.discord.com";
                    else
                        domain = "discord.com";

                    var webhook = JsonConvert.DeserializeObject<Webhook>(response);
                    ConsoleUtils.Log($"Webhook Bot ID: {webhook.id}");
                    ConsoleUtils.Log($"Webhook Name: {webhook.name}");
                    ConsoleUtils.Log($"Webhook Token: {webhook.token}");
                    ConsoleUtils.Log($"Webhook Type: {webhook.type}");
                    ConsoleUtils.Log($"Guild ID: {webhook.guild_id}");
                    ConsoleUtils.Log($"Channel ID: {webhook.channel_id}");
                    ConsoleUtils.Log($"Application ID: {(webhook.application_id == null ? "Not found." : webhook.application_id)}");
                    var widgetResponse = client.GetAsync($"https://{domain}/api/guilds/{webhook.guild_id}/widget.json").Result.Content.ReadAsStringAsync().Result;

                    if (widgetResponse == "{\"message\": \"Widget Disabled\", \"code\": 50004}")
                        ConsoleUtils.LogError($"Guild: {webhook.guild_id}'s widget is disabled. Couldn't resolve an invite.");
                    else
                    {
                        var widgetInfo = JsonConvert.DeserializeObject<Widget>(widgetResponse);
                        ConsoleUtils.Log("Widget Information:\n");
                        ConsoleUtils.Log($"Guild ID: {widgetInfo.id}");
                        ConsoleUtils.Log($"Guild Name: {widgetInfo.name}");
                        ConsoleUtils.Log($"Guild Online Count: {widgetInfo.presence_count}");
                        ConsoleUtils.Log($"Instant Invite: {widgetInfo.instant_invite}");
                        ConsoleUtils.Log($"Guild Channels:\n");

                        foreach (var channel in widgetInfo.channels)
                        {
                            ConsoleUtils.Log($"Channel ID: {channel.id}");
                            ConsoleUtils.Log($"Channel Name: {channel.name}");
                            ConsoleUtils.Log($"Channel Position: {channel.position}");
                        }

                        ConsoleUtils.Log($"Guild Members:\n");

                        foreach (var member in widgetInfo.members)
                        {
                            ConsoleUtils.Log($"User ID: {member.id}");
                            ConsoleUtils.Log($"User Name: {member.username}");
                            ConsoleUtils.Log($"User Discriminator: {member.discriminator}");
                            ConsoleUtils.Log($"User Avatar: {member.avatar}");
                            ConsoleUtils.Log($"User Avatar URL: {member.avatar_url}");
                        }
                    }
                }
            }
            catch(Exception e) {
                ConsoleUtils.LogError($"An error occurred! Exception Details:\n{e.ToString()}");
            }

            Console.ReadLine();
        }
    }
}

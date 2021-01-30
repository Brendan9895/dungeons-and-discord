# dungeons-and-discord
Multi-module discord bot with functionality for Dungeons and Dragons dice rolling and card drawing.

### Getting started
In order to run this bot, you will need to create a config.JSON file with the following properties contained within: 

{
  "Token": "",
  "BoundChannelId": ,
  "Prefix": "!",
  "BlacklistedChannels": [ ],
  "GameStatus": ""
}

Token will be your unique OAuth Bot Token, generated by the Discord Developer platform. It is a string value type.

BoundChannelId is the ID of the channel you want the bot to respond in. This takes a Ulong number value type.

Prefix is the character you must put before a message in order to issue a command to the bot. This takes a string value type.

BlacklistedChannels is a list of channel IDs where commands issued will be ignored by the bot. Takes an array of Ulong values. At least one value must be contained in the array.

GameStatus is not currently used but required to populate the POCO config class. Leave as empty string.

### Connecting to Lavalink Audio

In order for the music bot to work, you will need to establish a connection to the Lavalink web server. 

#### Prerequisites: 

Ensure you have a version of the Java JRE/SDK of at least version 11 installed.

Download the binaries from [here](https://ci.fredboat.com/viewLog.html?buildId=lastSuccessful&buildTypeId=Lavalink_Build&tab=artifacts&guest=1) 

Extract the binaries to your solution's Bin folder

Create an application.yml document in your solution's Bin folder. An example of this file's contents can be found [here](https://github.com/Frederikam/Lavalink/blob/master/LavalinkServer/application.yml.example)

#### Getting the bot to play music

Once you have followed the instructions above, navigate to your solution's bin folder via the command line and run the following command: `java -jar Lavalink.jar` to start the Lavalink webserver.

Now, when you start to debug the app, it should connect to your chosen server and when making calls to the music module commands, the bot should join the appropriate voice channel and perform the requested command.
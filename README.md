# Accessible Web Navigator

## Available Commands

### ExitCommand
- **Command**: `exit`
- **Purpose**: Exits the application. The text comparison is case-insensitive.

### NavigateCommand
- **Command**: `navigate to <URL>`
- **Purpose**: Navigates to the specified URL. If the URL does not start with "http://" or "https://", "https://" will be added as a prefix. Returns "Page loaded successfully." on success or "Failed to navigate to <URL>" on failure.

### AiCommand
- **Command**: `<prompt>`
- **Purpose**: Generates a clear, concise response based on the provided prompt and the web page content. The command reads the inner text content from the page's body, prepares a prompt for `OpenAiService`, and returns the generated response.

### ClickCommand
- **Command**: `<command input containing "click">`
- **Purpose**: Identifies the proper locator for the Playwright locator API using OpenAI's LLM capability and clicks on the identified HTML element. The command reads the HTML content from the page, uses the new method in `OpenAiService` to identify the locator, and clicks on the element using `LocatorClickOptions` with a timeout of 5 seconds. If the click call fails on `TimeoutException`, it returns "Failed to locate the correct web element to click."

### TypeCommand
- **Command**: `type <TEXT>`
- **Purpose**: Types the specified text into the page using Playwright's keyboard input. The command splits the input based on the format `type <TEXT>` to get the text value and uses Playwright to type the text into the page.

### TextToSpeechService
- **Purpose**: Converts the command output text into speech using OpenAI's text-to-speech capabilities with the Onyx voice. The audio is saved to `output.mp3` and played using the `NAudio` package. If the audio file already exists, it is deleted before saving the new audio. Error handling is included for failed text-to-speech calls, logging the error message to the console and writing "Text to speech failed." in the console. Audio playback can be canceled with a keyboard "Enter" key press.

### SpeechToTextService
- **Purpose**: Adds speech-to-text capability using OpenAI's whisper-1 model and NAudio package. Captures audio input using NAudio package and saves it to `input.mp3`. Transcribes captured audio using OpenAI's whisper-1 model and returns the transcribed text.

## Installation Instructions

### Install Playwright CLI
To install the Playwright CLI, run the following command:
```
dotnet tool install --global Microsoft.Playwright.CLI
```

### Install Browsers
To install the necessary browsers, run the following command:
```
playwright install
```

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

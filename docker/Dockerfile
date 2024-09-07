FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY publish/quiz-master/ ./
RUN chmod +x GameHub.QuizMaster
ENTRYPOINT ["./GameHub.QuizMaster"]
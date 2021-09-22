# Logging
To log informations/exceptions/etc, the backend is using the built in ILogger structure together with a custom database logger that logs everything into the database.

The reason for implementing an own custom logger instead of using NLog for example was to use the built in structure for logging stuff. NLog is using an own implementation for creating loggers.
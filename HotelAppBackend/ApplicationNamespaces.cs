//.NET
global using System;
global using Microsoft.Extensions.Options;
global using System.Collections.Generic;
global using System.Reflection;

//Nuget
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using Mongo.Context;

//FluentValidation
global using FluentValidation;
global using FluentValidation.AspNetCore;

//Customer
global using Hotels.Models;

global using Hotels.Configuration;

global using Hotels.Repositories;

global using Hotels.HotelService;

global using Hotels.Validators;

global using Hotels.GraphQL.Queries;

global using Hotels.GraphQL.Mutations;
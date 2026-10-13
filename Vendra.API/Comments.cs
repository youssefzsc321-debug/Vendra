using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Stripe;
using Stripe.Climate;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Vendra.API.Controllers;
using Vendra.Core.Common;
using Vendra.Core.Dtos.Identity;
using Vendra.Core.Dtos.OrderDtos;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Identity;
using Vendra.Core.Enities.Order_Aggragtte;
using Vendra.Core.Helpers;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.IdentityServices.Implementation;
using Vendra.Infrastructure.IdentityServices.Interfaces;
using Vendra.Infrastructure.Repositories;
using Vendra.Infrastructure.Specifications.Orders;
using Vendra.Infrastructure.Specifications.ProductSpec;

namespace Vendra.API
{
    public class Comments
    {
    }



}


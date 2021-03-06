﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class AddEmployeeTerritoryComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected NorthwindService Northwind { get; set; }

        IEnumerable<NorthwindBlazor.Models.Northwind.Employee> _getEmployeesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Employee> getEmployeesResult
        {
            get
            {
                return _getEmployeesResult;
            }
            set
            {
                if(!object.Equals(_getEmployeesResult, value))
                {
                    _getEmployeesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<NorthwindBlazor.Models.Northwind.Territory> _getTerritoriesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Territory> getTerritoriesResult
        {
            get
            {
                return _getTerritoriesResult;
            }
            set
            {
                if(!object.Equals(_getTerritoriesResult, value))
                {
                    _getTerritoriesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        NorthwindBlazor.Models.Northwind.EmployeeTerritory _employeeterritory;
        protected NorthwindBlazor.Models.Northwind.EmployeeTerritory employeeterritory
        {
            get
            {
                return _employeeterritory;
            }
            set
            {
                if(!object.Equals(_employeeterritory, value))
                {
                    _employeeterritory = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetEmployeesResult = await Northwind.GetEmployees();
            getEmployeesResult = northwindGetEmployeesResult;

            var northwindGetTerritoriesResult = await Northwind.GetTerritories();
            getTerritoriesResult = northwindGetTerritoriesResult;

            employeeterritory = new NorthwindBlazor.Models.Northwind.EmployeeTerritory();
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.EmployeeTerritory args)
        {
            try
            {
                var northwindCreateEmployeeTerritoryResult = await Northwind.CreateEmployeeTerritory(employeeterritory);
                DialogService.Close(employeeterritory);
            }
            catch (Exception northwindCreateEmployeeTerritoryException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new EmployeeTerritory!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}

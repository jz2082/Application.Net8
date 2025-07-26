import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { interval } from 'rxjs';
import { PrimengModule } from '@common/primeng.module';

import { AppInjectorService } from '@common/service/app-injector.service';
import { BaseComponent } from '@app/common/component/base-component.component';
import { StatisticComponent } from '../statistic/statistic.component';

const DEFAULT_COLORS = ['#3366CC', '#DC3912', '#FF9900', '#109618', '#990099',
  '#3B3EAC', '#0099C6', '#DD4477', '#66AA00', '#B82E2E',
  '#316395', '#994499', '#22AA99', '#AAAA11', '#6633CC',
  '#E67300', '#8B0707', '#329262', '#5574A6', '#3B3EAC']

@Component({
  selector: 'pluralsight-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,
    StatisticComponent
  ],
  providers: [
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent extends BaseComponent implements OnInit, AfterViewInit {
 @ViewChild('mixedChart') mixedChart: any;
  count: number = -1;

  hoursByProject = [
    { id: 1, name: 'Payroll App', hoursSpent: 8 },
    { id: 2, name: 'Agile Times App', hoursSpent: 16 },
    { id: 3, name: 'Point of Sale App', hoursSpent: 24 },
  ]
  chartOptions = {
    title: {
      display: true,
      text: 'Hours By Project'
    },
    legend: {
      position: 'bottom'
    },
  };
  pieLabels = this.hoursByProject.map((proj) => proj.name);
  pieData = this.hoursByProject.map((proj) => proj.hoursSpent);
  pieColors = this.configureDefaultColours(this.pieData);
  hoursByProjectChartData = {
    labels: this.pieLabels,
    datasets: [
      {
        data: this.pieData,
        backgroundColor: this.pieColors
      }
    ]
  }

  hoursByTeamChartData = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Dev Team',
        backgroundColor: DEFAULT_COLORS[0],
        data: [65, 59, 80, 55, 67, 73]
      },
      {
        label: 'Ops Team',
        backgroundColor: DEFAULT_COLORS[1],
        data: [44, 63, 57, 90, 77, 70]
      }
    ]
  }

  hoursByTeamChartDataMixed = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Dev Team',
        type: 'bar',
        backgroundColor: DEFAULT_COLORS[0],
        data: [65, 59, 80, 55, 67, 73]
      },
      {
        label: 'Ops Team',
        type: 'line',
        backgroundColor: DEFAULT_COLORS[1],
        data: [44, 63, 57, 90, 77, 70]
      }
    ]

  }

  constructor() {
    super(); 
    const injector = AppInjectorService.getInjector();
    if (injector == null) console.error('DashboardComponent - constructor - injector is null.');
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.loggerService.logTrace('DashboardComponent.ngOnInit()');
    this.module = 'DashboardComponent';
  }

  override ngAfterViewInit() {
    interval(3000).subscribe(() => {
      var hoursByTeam = this.hoursByTeamChartDataMixed.datasets;
      var randomised = hoursByTeam.map(dataset => {
        dataset.data = dataset.data.map(hours => hours = Math.random() * 100);
      });
      this.mixedChart.refresh();
    });
  }

  onDataSelect(event: { element: { _datasetIndex: any; _index: any; }; }) {
    let dataSetIndex = event.element._datasetIndex;
    let dataItemIndex = event.element._index;
    let labelClicked = this.hoursByTeamChartDataMixed.datasets[dataSetIndex].label;
    let valueClicked = this.hoursByTeamChartDataMixed.datasets[dataSetIndex].data[dataItemIndex];
    alert(`Looks like ${labelClicked} worked ${valueClicked} hours`);
  }

  private configureDefaultColours(data: number[]): string[] {
    let customColours: string[] = []
    if (data.length) {

      customColours = data.map((element, idx) => {
        return DEFAULT_COLORS[idx % DEFAULT_COLORS.length];
      });
    }
    return customColours;
  }
}

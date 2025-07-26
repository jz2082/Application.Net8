import { Component, OnInit } from '@angular/core';

import { LoggerService } from '@app/common/service/logger.service';

@Component({
  selector: 'common-footer',
  standalone: true,
  imports: [],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent implements OnInit {

  constructor(private loggerService: LoggerService) { 
  }

  ngOnInit(): void {
    this.loggerService.logTrace('FooterComponent.ngOnInit()');
  }
}

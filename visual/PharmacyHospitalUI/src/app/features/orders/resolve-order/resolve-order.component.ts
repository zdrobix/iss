import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-resolve-order',
  templateUrl: './resolve-order.component.html',
  styleUrls: ['./resolve-order.component.css']
})
export class ResolveOrderComponent implements OnInit, OnDestroy{

  constructor () {}

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}

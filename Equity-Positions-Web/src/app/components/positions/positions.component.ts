import { Component, OnInit } from '@angular/core';
import { PositionsService } from '../../services/positions.service';
import { Position } from '../../models/position.model';

@Component({
  selector: 'app-positions',
  templateUrl: './positions.component.html',
  styleUrls: ['./positions.component.css']
})
export class PositionsComponent implements OnInit {

  positions: Position[] = [];
  loading = false;
  errorMessage = '';

  constructor(private positionsService: PositionsService) {}
  
  ngOnInit(): void {
    this.loadPositions();
  }
  loadPositions(): void {
    this.loading = true;

    this.positionsService.getCurrentPositions().subscribe({
      next: (data) => {
        console.log('Positions from API:', data);
        this.positions = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading positions', err);
        this.errorMessage = 'Error loading positions';
        this.loading = false;
      }
    });
  }

}

export interface Transaction {
    tradeId:number,
    version:number,
    securityCode:string,
    quantity:number,
    action:string,
    buySell:string;
}
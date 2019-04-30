import { ActivatedRouteSnapshot, CanActivate, UrlTree, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";

@Injectable()
export class CanActivateAction implements CanActivate {
    constructor() {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        console.log("canActri")
        return true
    }
}

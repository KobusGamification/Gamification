<!DOCTYPE html>

<?php
	date_default_timezone_set("America/Sao_Paulo");
	$m = new MongoClient('mongodb://localhost');
	$m->connect();
	$db = $m->Teste;
	
	$collection = $db->IUser;
	$user = $collection->findOne(array('Name' => $_GET['name']));	
	
	$allBadgesCollection = $db->IBadge;
	$badge = $allBadgesCollection->find();
	$badge = $badge->sort(array("ExtensionName"));
	

function GetRank($cod){
	$path = 'res/Badges/Main/';
	$rank = '';
	switch($cod){
		case 1:
			$rank = 'diamond.png';
			break;
		case 2:
			$rank = 'corundum.png';
			break;
		case 3:
			$rank = 'topaz.png';
			break;
		default:
			$rank = 'quartz.png';			
	}
	
	return $path . $rank;
}
	
function getExtension($extension){
	$result = array();
	$keys = array_keys($extension);		
	
	if($extension["_t"] == "LanguageBuilder"){
		$languageAttr = $extension["LanguageAttributes"];
		$ks = array_keys($languageAttr);
		
		foreach($ks as $k){
			array_push($result, $languageAttr[$k]);
		}
	} else{
		foreach($keys as $key){		
			if(is_array($extension[$key])){
				if(array_key_exists("_t", $extension[$key])){
					return getExtension($extension[$key]);
				}
			}
		}	
		array_push($result, $extension);
	}
	
	
	return $result;
}
?>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Gamification</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body>

    <div id="wrapper">

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">Gamification</a>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">                               
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i>  <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="login.html"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">                        
                        <li>
                            <a href="index.html"><i class="fa fa-dashboard fa-fw"></i> Main</a>
                        </li>                        
                        <li>
                            <a href="status.php"><i class="fa fa-table fa-fw"></i> Status</a>
                        </li>                                               				                                         
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>

        <!-- Page Content -->
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header"><?php print $user["Name"]; ?></h1>
                </div>
				<div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Status
                        </div>
                        <div class="panel-body">
						
							<!-- Nav tabs -->
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#Levels" data-toggle="tab">Levels</a>
                                </li>
                                <li><a href="#Badges" data-toggle="tab">Badges</a>
                                </li>                                
                            </ul>
							
							
							<div class="tab-content">
								<div class="tab-pane fade in active" id="Levels">									
									<table class="table table-hover">
										<thead>
											<th>Status Name</th>
											<th>Current Experience</th>
											<th>Next Level</th>									
											<th>Current level</th>
										</thead>
										<tbody>
											<?php
												foreach($user["ExperiencePoints"] as $exp){
													if(!array_key_exists('_t', $exp)){
														$exp['_t'] = "Main Experience";
													}
													$currentExp = $exp["ExperiencePoints"];
													$next = $exp["ExperienceNextLevel"];									
													$percentForNextLevel = ($currentExp * 100) / $next;											
													
														print '<td>'.$exp["Alias"].'</td>';
																									
													
													print '<td>'.$exp["ExperiencePoints"].'</td>';											
													?>											
													<td>
													<div class="tooltip-demo progress progress-striped active">																														
														<div class="progress-bar progress-bar-default" data-toggle="tooltip" data-placement="top" title="<?php print 'Need ' . ($next - $currentExp) . ' exp points to reach next level.';?>" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print $percentForNextLevel?>%"></div>
													</div>		
													</td>
												
													<?php
													
													print '<td>'.$exp["Level"].'</td>';
													print '<tr />';
												}
											?>
										</tbody>
									</table>
									
									
				<?php
					
					
					foreach($user["ExtensionPoint"] as $extension){
						$exts = getExtension($extension);
						
						?>
							<div class="col-lg-4">
								<div class="panel panel-default">
									<div class="panel-heading">
										<?php print $extension["_t"];?>
									</div>						
						<?php
						$currentAlias = '';
						foreach($exts as $ext){
						
							if($currentAlias != $ext['_t']){								
						?>									
								
									<div class="panel-body">
										<table class="table table-hover">
										<thead>
											<?php
												$ks = array_keys($ext);												
												foreach($ks as $k){
													if($k != '_t' and !is_array($ext[$k])){
														print '<th>'. $k . '</th>';
													}													
												}
											?>										
										
										</thead>
										<tbody>
										<?php 										 										
										} else{
											print '<tr />';
										}
											
											foreach($ks as $k){																								
												if($k != '_t'){
													if(!is_array($ext[$k])){																												
														print '<td>' . $ext[$k] . '</td>';														

													}
												}
												
											}
											
										?>
										<?php
											if(sizeof($exts) > 1){
												if($currentAlias != $ext["_t"]){
													$currentAlias = $ext['_t'];							
													continue;
												}
											}
										?>
										</tbody>
										</table>
									</div>                        							
								</div>
							</div>      	
						<?php							
						}
					}
				?>			
			
									
								</div>								
									
								
								
								<div class="tab-pane fade" id="Badges">									
									<table class="table table-hover">
										<thead>
											<th>Total Badges</th>											
											<th>Total Diamond Badges</th>
											<th>Total Curundum Badges</th>											
											<th>Total Topaz Badges</th>
											<th>Total Quartz Badges</th>											
										</thead>
										<tbody>
											<td>
												<div class="tooltip-demo progress progress-striped active">																														
													<div class="progress-bar progress-bar-default" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
												</div>		
											</td>
											<td>
												<div class="tooltip-demo progress progress-striped active">																														
													<div class="progress-bar progress-bar-default" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
												</div>		
											</td>
											<td>
												<div class="tooltip-demo progress progress-striped active">																														
													<div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
												</div>					
											</td>
											<td>
												<div class="tooltip-demo progress progress-striped active">																														
													<div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
												</div>					
											</td>
											<td>
												<div class="tooltip-demo progress progress-striped active">																														
													<div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
												</div>					
											</td>
										</tbody>
									</table>
									<ul class="nav nav-tabs">
									<?php 
										$currentSection = '';
										foreach($badge as $b){
											if($currentSection != $b["ExtensionName"]){												
												print '<li><a href="#' . $b["ExtensionName"] . '" data-toggle="tab">' . $b["ExtensionName"] . '</a></li>';
												$currentSection = $b["ExtensionName"];	
											}											
										}
										
									?>										
									</ul>
									
									<?php 
										$first = true;
										$currentSection = '';										
										$perLine = 0;
										foreach($badge as $b){
											
											$currentBadge = null;											
											if($currentSection != $b["ExtensionName"]){																								
												$currentSection = $b["ExtensionName"];
												if(!$first){
													print '</div>';
													print '</div>';													
												} else{
													$first = false;													
												}
											$usrBadges = sizeof($user["Badges"]);
											$bdgs = $allBadgesCollection->count();																								
											$percentEarned = ($usrBadges * 100) / $bdgs;																	
											?>
											
												<div class="tab-content">
													<div class="tab-pane " id="<?php print $b["ExtensionName"]; ?>">																											
														<table class="table table-hover">
															<thead>
																<th>Total Badges</th>											
																<th>Total Diamond Badges</th>
																<th>Total Curundum Badges</th>											
																<th>Total Topaz Badges</th>
																<th>Total Quartz Badges</th>											
															</thead>
															<tbody>
																<td>
																	<div class="tooltip-demo progress progress-striped active">																														
																		<div class="progress-bar progress-bar-default" data-toggle="tooltip" data-placement="top" title="<?php print $usrBadges . '/' . $bdgs;?>" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print $percentEarned?>%"></div>
																	</div>				
																</td>
																<td>
																	<div class="tooltip-demo progress progress-striped active">																														
																		<div class="progress-bar progress-bar-default" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
																	</div>		
																</td>
																<td>
																	<div class="tooltip-demo progress progress-striped active">																														
																		<div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
																	</div>					
																</td>
																<td>
																	<div class="tooltip-demo progress progress-striped active">																														
																		<div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
																	</div>					
																</td>
																<td>
																	<div class="tooltip-demo progress progress-striped active">																														
																		<div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: <?php print '60'?>%"></div>
																	</div>					
																</td>
															</tbody>
														</table>																											
													<table class="table">
													<tbody>
													
											<?php
											}												
												if(array_key_exists("Badges", $user)){
													foreach($user["Badges"] as $userBadge){
														if($userBadge["Name"] == $b["Name"]){
															$currentBadge = $userBadge;
															break;
														}
													}
												}												
												?>

												<div class="modal fade" id="<?php print $b["_t"]; ?>" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
													<div class="modal-dialog">
														<div class="modal-content">
															<div class="modal-header">
																<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
																<h4 class="modal-title" id="myModalLabel">																
																<?php 																																		
																	if($currentBadge != null || $b["Secret"] != true){
																		?>
																	<img src="<?php print getRank($b["Level"]); ?>" />
																	<?php
																		print $b["Name"];
																	} else {
																		print 'Badge Oculto';
																	}
																
																?></h4>
															</div>
															<div class="modal-body">
																<?php 																	
																	if($currentBadge != null || $b["Secret"] != true){
																		print $b["Content"]; 
																	} else{
																		print 'Oculto';
																	}
																?>
															</div>
															<div class="modal-footer">																							
																
																<?php 																	
																	if($currentBadge != null){																	
																	?>
																	<div class="alert alert-success">
																		<?php
																			print 'Earned at : ' . (date("F j, Y, g:i a", $userBadge["DateEarned"]->sec));
																		?>
																	</div>
																	<?php
																	}																
																?>
																
															</div>
														</div>
														<!-- /.modal-content -->
													</div>
													<!-- /.modal-dialog -->
												</div>
							
														
												<?php
												if($perLine > 2){
													$perLine = -1;
													print '<tr />';
												}
												?>
												
												
												
												<?php
												
												if($currentBadge != null){												
													print '<td><button data-toggle="modal" data-target="#' . $b["_t"] . '" class="btn btn-outline btn-success"><img src="' . $b["IconPath"] . '"/></button></td>';													
												} else{
													if($b["Secret"] == true){
														print '<td><button data-toggle="modal" data-target="#' . $b["_t"] . '" class="btn btn-outline btn-default"><img src="res/Badges/Main/Locked.png"/></button></td>';													
													} else{
														print '<td><button data-toggle="modal" data-target="#' . $b["_t"] . '" class="btn btn-outline btn-default"><img src="' . $b["IconPath"] . '"/></button></td>';													
													}
												}
												
												
																						
										
									?>
										
									<?php
										$perLine++;
										}			
									?>	
										</tbody>
										</table>			
										</div>
									</div>									
								</div>
							</div>
							
                        </div>                        
                    </div>
                </div>      									
        </div>        
    </div>
    <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/sb-admin-2.js"></script>
	<script>
		// tooltip demo
		$('.tooltip-demo').tooltip({
			selector: "[data-toggle=tooltip]",
			container: "body"
		})

		// popover demo
		$("[data-toggle=popover]")
			.popover()
    </script>
</body>

</html>


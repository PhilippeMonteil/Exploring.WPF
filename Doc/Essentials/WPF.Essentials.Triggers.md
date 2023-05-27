
# Triggers

## Property Triggers

     <Style.Triggers>

       <Trigger Property="IsMouseOver" Value="True">
         <Setter Property="" Value="" />
       </Trigger>

       <Trigger Property="Validation.HasErrors" Value="True">
         <Setter Property="Background" Value="Red" />
         <Setter Property="Tooltip" 
            Value="{Binding 
                    RelativeSource={RelativeSource Self}},
                    Path={Vaidation.Errors[0].ErrorContent} " />
       </Trigger>

     </Style.Triggers>

## Data Triggers

     <Style.Triggers>

       <DataTrigger 
            Binding="{Binding RelativeSource={RelativeSource Self}, Path=Text}" 
            Value="True">
         <Setter Property="IsEnabled" Value="False" />
       </DataTrigger>

     </Style.Triggers>

## MultiTrigger

     <Style.Triggers>

       <MultiTrigger>

         <MultiTrigger.Conditions>
           <Condition Property="IsMouseOver" Value="True"/>
           <Condition Property="IsFocused" Value="True"/>
         </MultiTrigger.Conditions>

         <Setter Property="IsEnabled" Value="False" />

       </MultiTrigger>

     </Style.Triggers>

## Event Trigger

     <Button.Triggers>
       <EventTrigger RoutedEvent="Button.Click">
         <EventTrigger.Actions>
           <BeginStoryBoard>
             <StoryBoard>

             </StoryBoard>
           </BeginStoryBoard>
         </EventTrigger.Actions>
       </EventTrigger>
     </Button.Triggers>
